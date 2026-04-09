// C# Learning Lab - frontend controller.
// No framework, no build step. Talks to a Blazor WebAssembly runtime via JSInterop.
// The [JSInvokable] methods it calls live in CSharpLearningLab/Services/JsBridge.cs.

const ASSEMBLY = "CSharpLearningLab";

const state = {
  lessons: [],
  currentId: null,
  currentLesson: null,
  completed: loadCompleted(),
};

// ---------- .NET bridge ----------
// Wraps DotNet.invokeMethodAsync and unwraps the JSON string that JsBridge returns.
async function invokeDotNet(method, ...args) {
  const json = await DotNet.invokeMethodAsync(ASSEMBLY, method, ...args);
  return json === "null" ? null : JSON.parse(json);
}

// Resolves once the Blazor WASM runtime has fully booted and JsBridge.Initialize has run.
// App.razor.cs fires a `blazor:ready` window event in its first OnAfterRenderAsync.
function waitForBlazor() {
  return new Promise((resolve) => {
    // If DotNet is already present, Blazor is up — resolve immediately.
    if (typeof DotNet !== "undefined") {
      resolve();
      return;
    }
    window.addEventListener("blazor:ready", () => resolve(), { once: true });
  });
}

// ---------- persistence ----------
function loadCompleted() {
  try {
    const raw = localStorage.getItem("cs-lab-completed");
    return raw ? new Set(JSON.parse(raw)) : new Set();
  } catch {
    return new Set();
  }
}

function saveCompleted() {
  localStorage.setItem("cs-lab-completed", JSON.stringify([...state.completed]));
}

// ---------- bootstrap ----------
async function init() {
  // Block until the .NET runtime is up and JsBridge.Initialize has run.
  await waitForBlazor();

  try {
    state.lessons = await invokeDotNet("ListLessons");
  } catch (err) {
    document.getElementById("lesson-list").innerHTML =
      `<div class="sidebar-loading">Failed to load lessons: ${err.message}</div>`;
    return;
  }

  renderSidebar();
  updateProgress();

  // Auto-open lesson from URL hash, or the first lesson, or the last-visited one.
  const hashId = location.hash.replace(/^#/, "");
  const firstId = state.lessons[0]?.id;
  const target = hashId || localStorage.getItem("cs-lab-last") || firstId;
  if (target) loadLesson(target);

  window.addEventListener("hashchange", () => {
    const id = location.hash.replace(/^#/, "");
    if (id && id !== state.currentId) loadLesson(id);
  });
}

// ---------- sidebar ----------
function renderSidebar() {
  const el = document.getElementById("lesson-list");
  el.innerHTML = "";

  // Group by category while preserving order.
  const groups = [];
  for (const l of state.lessons) {
    let g = groups.find((x) => x.name === l.category);
    if (!g) {
      g = { name: l.category, lessons: [] };
      groups.push(g);
    }
    g.lessons.push(l);
  }

  for (const g of groups) {
    const header = document.createElement("div");
    header.className = "category";
    header.textContent = g.name;
    el.appendChild(header);

    for (const l of g.lessons) {
      const item = document.createElement("div");
      item.className = "lesson-item";
      if (l.id === state.currentId) item.classList.add("active");
      if (state.completed.has(l.id)) item.classList.add("done");
      item.dataset.id = l.id;
      item.innerHTML = `
        <span class="lesson-num">${String(l.order).padStart(2, "0")}</span>
        <span class="lesson-title-s">${escapeHtml(l.title)}</span>
        <span class="status-dot"></span>
      `;
      item.addEventListener("click", () => {
        location.hash = l.id;
        loadLesson(l.id);
      });
      el.appendChild(item);
    }
  }
}

function updateProgress() {
  const total = state.lessons.length;
  const done = state.lessons.filter((l) => state.completed.has(l.id)).length;
  document.getElementById("progress-label").textContent = `${done} / ${total} complete`;
  document.getElementById("progress-fill").style.width =
    total ? `${(done / total) * 100}%` : "0%";
}

// ---------- lesson view ----------
async function loadLesson(id) {
  state.currentId = id;
  localStorage.setItem("cs-lab-last", id);

  // Update sidebar highlight.
  document.querySelectorAll(".lesson-item").forEach((el) => {
    el.classList.toggle("active", el.dataset.id === id);
  });

  const view = document.getElementById("lesson-view");
  view.innerHTML = `<div class="empty-state"><p>Loading lesson…</p></div>`;

  try {
    const lesson = await invokeDotNet("GetLesson", id);
    if (!lesson) throw new Error("lesson not found");
    state.currentLesson = lesson;
  } catch (err) {
    view.innerHTML = `<div class="empty-state"><p>Failed to load lesson: ${err.message}</p></div>`;
    return;
  }

  renderLesson(state.currentLesson);
  // Scroll content back to top whenever a new lesson loads.
  view.scrollTop = 0;
  window.scrollTo({ top: 0, behavior: "instant" });
}

function renderLesson(lesson) {
  const view = document.getElementById("lesson-view");
  const isDone = state.completed.has(lesson.id);

  view.innerHTML = `
    <div class="lesson-header">
      <span class="lesson-order">Lesson ${String(lesson.order).padStart(2, "0")}</span>
      <span class="lesson-category">${escapeHtml(lesson.category)}</span>
    </div>
    <h1 class="lesson-title">${escapeHtml(lesson.title)}</h1>

    <div class="callout ts">
      <div class="label">TypeScript → C#</div>
      <div class="explanation">${renderMarkdown(lesson.tsAnalogy)}</div>
    </div>

    <div class="explanation">${renderMarkdown(lesson.explanation)}</div>

    <div class="editor-section">
      <h3>Try it — this code runs on your machine</h3>
      <div class="editor-wrap">
        <div class="editor-toolbar">
          <span class="filename">playground.csx</span>
          <div class="buttons">
            <button id="btn-reset" title="Reset to the starter code">Reset</button>
            <button id="btn-run" class="primary">▶ Run (Ctrl+Enter)</button>
          </div>
        </div>
        <textarea id="code-editor" class="editor" spellcheck="false"></textarea>
      </div>
      <div class="output-panel">
        <div class="output-header">
          <span>Output</span>
          <span id="output-time"></span>
        </div>
        <div id="output-body" class="output-body idle">Hit Run to execute this snippet.</div>
      </div>
    </div>

    ${lesson.keyPoints?.length ? renderKeyPoints(lesson.keyPoints) : ""}
    ${lesson.quizzes?.length ? renderQuizzes(lesson.quizzes) : ""}

    <div class="mark-done">
      <button id="btn-done" class="primary">
        ${isDone ? "✓ Completed — mark incomplete" : "Mark lesson complete"}
      </button>
      ${isDone ? '<span class="done-badge">You\'ve completed this lesson</span>' : ""}
    </div>
  `;

  const editor = document.getElementById("code-editor");
  editor.value = lesson.starterCode;

  // Tab key should insert 4 spaces, not blur the textarea.
  editor.addEventListener("keydown", (e) => {
    if (e.key === "Tab") {
      e.preventDefault();
      const start = editor.selectionStart;
      const end = editor.selectionEnd;
      editor.value =
        editor.value.slice(0, start) + "    " + editor.value.slice(end);
      editor.selectionStart = editor.selectionEnd = start + 4;
    } else if (e.key === "Enter" && (e.ctrlKey || e.metaKey)) {
      e.preventDefault();
      runCode();
    }
  });

  document.getElementById("btn-run").addEventListener("click", runCode);
  document.getElementById("btn-reset").addEventListener("click", () => {
    editor.value = lesson.starterCode;
    setOutput("Editor reset to starter code.", "idle");
  });
  document.getElementById("btn-done").addEventListener("click", toggleDone);

  // Wire up quizzes.
  document.querySelectorAll(".quiz").forEach((quizEl, idx) => {
    const quiz = lesson.quizzes[idx];
    const choices = quizEl.querySelectorAll(".quiz-choice");
    const expl = quizEl.querySelector(".quiz-explanation");
    choices.forEach((btn, i) => {
      btn.addEventListener("click", () => {
        choices.forEach((b) => (b.disabled = true));
        if (i === quiz.correctIndex) {
          btn.classList.add("correct");
        } else {
          btn.classList.add("wrong");
          choices[quiz.correctIndex].classList.add("correct");
        }
        expl.style.display = "block";
      });
    });
  });
}

function renderKeyPoints(points) {
  const items = points.map((p) => `<li>${renderInline(p)}</li>`).join("");
  return `
    <div class="key-points">
      <h3>Key takeaways</h3>
      <ul>${items}</ul>
    </div>
  `;
}

function renderQuizzes(quizzes) {
  return quizzes
    .map(
      (q) => `
    <div class="quiz">
      <div class="quiz-question">${escapeHtml(q.question)}</div>
      <div class="quiz-choices">
        ${q.choices
          .map((c) => `<button class="quiz-choice">${escapeHtml(c)}</button>`)
          .join("")}
      </div>
      <div class="quiz-explanation" style="display:none">
        <strong>Explanation:</strong> ${escapeHtml(q.explanation)}
      </div>
    </div>
  `
    )
    .join("");
}

// ---------- code execution ----------
async function runCode() {
  const editor = document.getElementById("code-editor");
  const runBtn = document.getElementById("btn-run");
  const code = editor.value;

  runBtn.disabled = true;
  runBtn.textContent = "Running…";
  setOutput("Running…", "idle");

  try {
    const data = await invokeDotNet("RunCode", code);

    document.getElementById("output-time").textContent =
      data.elapsedMs != null ? `${data.elapsedMs}ms` : "";

    if (data.success) {
      const text = data.output?.length ? data.output : "(no output)";
      setOutput(text, "ok");
    } else {
      const parts = [];
      if (data.output?.length) parts.push(data.output);
      if (data.error) parts.push(data.error);
      setOutput(parts.join("\n"), "error");
    }
  } catch (err) {
    setOutput(`Request failed: ${err.message}`, "error");
  } finally {
    runBtn.disabled = false;
    runBtn.textContent = "▶ Run (Ctrl+Enter)";
  }
}

function setOutput(text, kind) {
  const body = document.getElementById("output-body");
  body.textContent = text;
  body.className = `output-body ${kind}`;
}

// ---------- completion toggle ----------
function toggleDone() {
  const id = state.currentId;
  if (!id) return;
  if (state.completed.has(id)) state.completed.delete(id);
  else state.completed.add(id);
  saveCompleted();
  renderSidebar();
  updateProgress();
  renderLesson(state.currentLesson);
}

// ---------- tiny markdown-ish renderer ----------
// Handles code fences, inline code, line breaks, and escaping. Not a real markdown
// parser — just enough to make the lesson prose readable.
function renderMarkdown(text) {
  if (!text) return "";
  // Extract triple-backtick code blocks first so we don't double-escape their content.
  const blocks = [];
  text = text.replace(/```(?:\w+)?\n([\s\S]*?)```/g, (_, body) => {
    blocks.push(body);
    return `\u0000BLOCK${blocks.length - 1}\u0000`;
  });

  let html = escapeHtml(text);
  html = html.replace(/`([^`]+)`/g, (_, c) => `<code>${c}</code>`);
  html = html.replace(/\n/g, "<br>");

  // Re-insert code blocks (they were already escaped by blockCode).
  html = html.replace(/\u0000BLOCK(\d+)\u0000/g, (_, i) => {
    return `<pre><code>${escapeHtml(blocks[Number(i)])}</code></pre>`;
  });
  return html;
}

// For key points: inline backticks only, no line breaks.
function renderInline(text) {
  let html = escapeHtml(text);
  html = html.replace(/`([^`]+)`/g, (_, c) => `<code>${c}</code>`);
  return html;
}

function escapeHtml(s) {
  return String(s)
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;")
    .replace(/'/g, "&#39;");
}

// ---------- go ----------
init();
