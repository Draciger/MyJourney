# PGR307 – Agile Project: Peers Event Platform

A group project developing an AI-assisted event management platform for Peers — a real-time voting and scoring service for competitions, sports events, and live shows. The project was conducted over four weeks using an agile approach combining a Google Design Sprint and three Scrum sprints.

---

## Project Vision

Make it effortless for event planners to go from "I want to create an event" to "my event is ready" — with flexible setup options, minimal friction, and maximum clarity. The goal was to reduce onboarding confusion so that new event makers can confidently set up events in Peers without external help.

---

## Solution — Peers Web Application

A full-stack web application that lets event organizers create, configure, and manage events with three distinct setup paths.

### Setup Options

**AI-Assisted Setup** — A chat-based flow powered by Google Gemini. The user describes their event in natural language and the AI extracts details such as name, venue, date, and scoring format, populating a live checklist. The user can review and edit all suggested values before saving.

**Manual Setup** — A structured form with dropdowns and text fields for users who want direct control. A live preview on the right side updates instantly as fields are filled in.

**Import from File** — Users can upload a file (e.g. a PDF) and the system extracts relevant event details to pre-fill the setup, which can then be completed via AI or manual flow.

### Key Features

**Scoring Templates** — Three templates to choose from at the start of event creation, each suited to different event formats:
- **Ranking** — orders athletes from best to worst based on aggregated scores
- **Battle** — head-to-head matchups across rounds, with the crowd or panel picking the winner
- **Poll** — quick single-choice voting on one athlete or option

**Judging Settings** — Configurable per template. Includes score range, judging duration, live leaderboard toggle, judging groups (Audience, Expert Panel, Athletes), scoring weights per group, and selectable judging criteria such as creativity, technique, execution, and more.

**Score Formats** — Score voting (panel assigns numeric scores), scream voting (crowd energy-based), and like voting (simple reactions for polls).

**Event Summary** — Review page before saving, with options to upload an event image and import participant lists via CSV. Events can be saved as drafts and returned to later.

**Dashboard** — Displays all active and in-progress events with status, location, start date, participant count, and a unique event code. Events can be edited or deleted directly from the dashboard.

**FAQ Widget** — Floating help widget on the landing page with collapsible answers to common questions about the platform.

---

## Delivery Test Results

All core user flows passed end-to-end testing. A full test log is available in `Appendix_F_Delivery_Tests.pdf`. Summary:

- All three event templates select and continue correctly
- AI-assisted setup handles varied inputs including vague language and long titles
- Manual form updates live preview in real time
- Judging settings open and apply correctly across all template types
- Event summary saves to dashboard with a unique event code
- Edit and delete functions work as expected
- Frontend-to-backend integration stable with no API errors observed

The only known out-of-scope feature is the "Join Event" button on the landing page.

---

## Process

### Week 1 — Google Design Sprint
Mapped the existing Peers event creation flow, identified friction points through expert interviews and HMW (How Might We) questions, voted on focus areas, and built a testable prototype. User testing validated the concept and highlighted areas to improve: clearer terminology, a better landing page, and a smoother file upload flow.

### Week 2 — Sprint 1: Foundation
Established the project structure, set up the database, connected the AI agent to the backend, and built out key frontend pages including the dashboard. Defined the Definition of Done and acceptance criteria, used Planning Poker for estimation, and managed work in Scrumwise.

### Week 3 — Sprint 2: Integration
Merged the frontend and backend into a unified, working system. Connected the AI agent to the frontend, added CSV/Excel participant list handling, refined the UI, and resolved integration issues across team members' environments.

### Week 4 — Sprint 3: Polish & Delivery
Fixed remaining bugs, completed nice-to-have features, conducted a live demo with the Peers product owner (received positively), finalized the written report, and completed delivery testing.

---

## Tech Stack

| Layer | Technology |
|-------|------------|
| Frontend | React + Vite |
| Backend | Python + FastAPI |
| Database | SQLite + SQLAlchemy ORM |
| AI Assistant | Google Gemini (function calling) |
| Version Control | GitHub |
| Project Management | Scrumwise |
| Hosting | Vercel (frontend) |

---

## Project Structure

```
agileproject-exam/
├── frontend/        # React + Vite application
├── backend/         # FastAPI + SQLAlchemy backend
│   ├── crud.py
│   ├── db/schema    # Database models (Event, Participant, EventImage, etc.)
│   └── events.db    # SQLite database file
└── README.md        # Setup instructions
```

---

## Running Locally

Unpack `agileproject-exam.zip`, open the project in your IDE, and follow the setup steps in the project's `README.md` file.
