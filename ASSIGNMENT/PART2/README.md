

---

# 🤖 CyberGuard — Cyber Awareness Chatbot

## 📋 Description

CyberGuard is a terminal-inspired Windows Forms desktop chatbot built in C#. It is specifically engineered to interactively educate users on foundational cybersecurity pillars—including **Password Safety**, **Phishing Awareness**, and **Safe Browsing practices**.

---

## 🛠️ Setup Instructions

1. **Open Visual Studio** (2019/2022 recommended).
2. **Create a new project** choosing **Windows Forms App (.NET Framework 4.8)**.
3. **Replace the generated template files** with the provided source code variants.
4. **Import Audio Dependencies:** Add your `greet.wav` directly into the root layout directory, and toggle its properties panel to:
> `Copy to Output Directory` = **Copy Always**


5. **Compile & Deploy:** Press `F5` to build and initiate the software canvas.

---

## 🕹️ How to Use

* **Onboarding:** Type your name when prompted by the identity registration filters.
* **Navigation:** Interact via the structured numbered navigation layout controls or simply type freely in natural language.
* **Contextual Streams:** Type keywords like `"tell me more"`, `"explain more"`, or `"another tip"` to receive seamless, non-breaking educational follow-up info.
* **Termination:** Input `"exit"` or selection option `4` to break the active loop and reveal your customized user engagement metric metrics.

---

## ✨ Features

* **Keyword Recognition Engine:** Scans string queries dynamically across 12 distinct system security tokens.
* **Heuristic Sentiment Detection:** Actively scales user emotional states to adjust responses dynamically based on indicators (*Worried*, *Confused*, *Frustrated*, *Curious*).
* **Asynchronous Tip Handlers:** Utilizes clean `TipProvider` delegates to serve randomized safety tips safely out of memory pools.
* **Session Recall Vault:** Tracks exact active dialogue metrics alongside a time-spent analysis module to deduce your absolute favorite subject.
* **Multimedia Fallbacks:** Implements custom system voice greetings instantly upon GUI instantiation.
* **Fixed Monospace Canvas:** Embedded, programmatically centered ASCII geometric logo framing.

---

## 📂 Project Architecture & Component Map

The application separates concerns by dividing the user interface event management from the core chatbot business logic, visual design rules, and internal tracking models:

| Component / File | Class Type | Core Responsibility |
| --- | --- | --- |
| **`Program.cs`** | `Static Class` | Main framework configuration hook; controls the application thread lifecycle. |
| **`CyberForm.cs`** | `Partial Form Class` | WinForms UI layer; manages the visual canvas window bounds and intercepts user hardware input events (like hitting `Enter`). |
| **`CyberDesign.cs`** | `Base Class` | The visual engine; handles message box painting, custom RGB color themes, and centers the escaped ASCII geometric banner logo. |
| **`CyberSpace.cs`** | `Derived Class` | The brain of the chatbot (inherits `CyberDesign`); controls conversation state routing (`main`, `password`, `phishing`), text parsing, and keyword matching. |
| **`CyberUser.cs`** | `Helper Class` | Manages the session profiles; tracks volatile user states, active input filtration, educational question counters, and ongoing time accumulation metrics. |
| **`CyberTips.cs`** | `Data Repository` | Hosts the static datasets; provides arrays of tips via `TipProvider` delegates, sentiment mapping matrices, and baseline phrase lists. |

---

## 📺 Presentation & Repository

* **YouTube Presentation:** [https://youtu.be/2qPm7KymUD4]
* **GitHub Repository:** [https://github.com/Oregolele05/PROG6221_POE/edit/master/ASSIGNMENT/PART2]
