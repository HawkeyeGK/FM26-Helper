# FM26-Helper Roadmap & Backlog

## Overview
This document serves as the high-level architectural vision for FM26-Helper.
Detailed user stories, prioritization, and task tracking are managed in **GitHub Projects**.

## Current Focus: Phase 3 (UI & Foundation)
**Goal:** Transform the static roster list into an interactive dashboard.
* **Interactive Grids:** Implement sorting and filtering for the main Roster Table.
* **Player Detail Card:** Create a detailed view (Modal or separate page) displaying Raw Attributes alongside Analysis.
* **Visual Polish:** Implement heatmaps (color-coding) for high attributes and scores.
* **Tech Debt:** Establish a CSS strategy and UI component library.

---

## Functional Domains (Epics)

### 1. Tactic Workshop (Analysis)
*Focus: Identifying the shape of the team.*
* **Best Role Analysis:** Aggregate view showing player counts per role (e.g., "3 Excellent Poachers").
* **Depth Chart:** Visualizing the roster grouped by position/role (Starter vs. Backup).
* **Tactic Definition:** Create/Save "Target Tactics" (e.g., "4-2-3-1 Gegenpress") to drive analysis.

### 2. Squad Management (Execution)
*Focus: Managing the active roster against the chosen tactic.*
* **Tactical Roster View:** Filtered grid showing only players fitting the "Target Tactic."
* **Player Detail Card:** Drill-down view for specific players showing Raw Stats + Analysis.
* **Gap Analysis:** Highlighting positions in the "Target Tactic" where no suitable player exists.

### 3. Data Administration (Housekeeping)
*Focus: CRUD operations, Schema, and Metadata.*
* **Edit Player:** UI to fix OCR errors or update attributes manually.
* **Add/Remove Player:** Controls to manually add a player or delete one (e.g., sold/released).
* **Player Annotations:** Tags for "Transfer Listed," "Loan," "Key Player," etc.
* **Goalkeeper Support:** Update Data Models to include GK-specific attributes (Reflexes, Handling).
* **Manual Import:** Upload screenshot/profile for existing players to update stats.

### 4. Scouting Network (External Data)
*Focus: Analyzing players not on the team.*
* **Scouting Database:** Separate storage for non-roster players.
* **Fit Calculator:** Score scouted players against the "Target Tactic."
* **Head-to-Head:** Side-by-side comparison (Scout vs. Starter).

### 5. Player Lab (Progression)
*Focus: Time-series analysis.*
* **Evolution Tracking:** Store historical snapshots keyed by `GameDate`.
* **Progression Graph:** Visualizing attribute changes over time (Deltas).

### 6. User Interface & Foundation
*Focus: UX Standards and Technical Plumbing.*
* **CSS Architecture:** Standardize styling approach (Global vs. Scoped, Variables).
* **Interactive Tables:** Reusable component for Sorting, Filtering, and Paging.
* **Visual Language:** Consistent color-coding (Heatmaps) for attributes (1-20 scale) and Scores (0-100%).