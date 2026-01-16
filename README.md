# Observe — Hell Let Loose Competitive Post-Match Analysis Tool

**Observe** is a lightweight background tool for *Hell Let Loose* competitive players. During a match it captures **screenshots of the in-game map** (only when the map is **fully zoomed out**) and stamps each screenshot with a **previously NTP-synchronized timestamp**.

After the match, players can **collect all screenshots** (from all team members) and use Observe to **render them into a timestamp-accurate MP4 video**. This makes it possible to analyze positioning, rotations, timings, and team strategy **frame by frame**.

> **Origin:** Built while I was a member of the clan **Omen** to support tactical analysis.  
> **Note:** This is a hobby project — the code is not “enterprise-grade”. 🙂

---

## What Observe Does

- No Memory Access!
- Runs in the background while you play
- Detects when the **map is opened** and **fully zoomed out**
- Captures a screenshot of the map and save it as webp
- Adds an **NTP-synchronized timestamp**
- Stores screenshots in `snapshots/<date_time>/...`
- After the match, converts and stitches screenshots into a **timeline-accurate** video

---

## How to Get It

You can either:
- **Build from source** (compile the code from GitHub), or
- Use **precompiled releases**

> The upload server structure/workflow will be published later (planned for GitHub after cleanup).

---

## Requirements & Limitations

### Required Setup Conditions
- **Do not use heavy NVIDIA filters**  
  (They can interfere with detection and also reduce video readability later.)
- **Do not play Commander role**  
  The Commander UI shifts the map position; Observe does not support this.
- Screenshots are captured **only when the map is fully zoomed out**

---

## Setup Guide (Teach-In / Calibration)

1. Start Observe.
2. Join an **empty server** (and **avoid a server running the Map `Hill 400`**).
3. Create a squad.
   - **Do not** take the Commander role.
4. Spawn in.
5. Open the map.
6. Alt-tab out of the game (or place Observe on a second monitor).
7. Press **`Teachin`** and confirm with **Yes** to start.
8. Alt-tab back into the game.
9. Audio signals:
   - After ~5 seconds: **1× beep** → Teach-In started
   - When finished: **2× beeps** → Teach-In completed

### Teach-In Quality Indicator
- The tool shows **Offset Points**
- A good Teach-In usually results in **Offset Points between 15 and 70**

---

## During the Match (Recording)

1. Start Observe at any time (recommended: **before match start**).
2. Enable the **Run** checkbox.
3. Observe will now run in the background:
   - Whenever you open the map, it captures a map screenshot
   - Saves to:
     - `snapshots/`
     - plus a subfolder with **date and time**

### Critical Warning: Warmup / Dry Run
✅ Make sure you **do not enable Run during warmup/dry run**, otherwise the final rendered video timeline becomes **wrong / corrupted**.

---

## After the Match (Video Rendering)

1. Collect all screenshots so that **one user has the complete set**.
2. Put all screenshots into **one folder**.
3. In Observe, click **Video Creator**.

Behavior depends on configuration (see `VarGlobal.vb`):
- If `WebVideoCreatorEnable = false` → opens the **offline** creator
- Otherwise → opens the web page (the `auth.php` endpoint of the web video creator)

4. Choose the folder with the collected screenshots.
5. Observe will:
   - Convert `WEBP` → `PNG`
   - Sort and align screenshots in **timestamp order**
   - Optionally render overlays into each image (if enabled):
     - timeline
     - timestamp

6. Output: an **`.mp4`** file that you can play back and analyze.

---

## Known Issues / Important Notes

- This is a hobby tool — **don’t blame me for code quality**.
- **Teach-In on `Hill 400`** produces false-positive Offset Points (unknown cause).
- **Commander role is not supported** (Teach-In and Run).
- Teach-In may fail if:
  - too heavy NVIDIA filters are used (detection + video readability suffers)
- If the map is opened **too briefly**, **real in-game** (non-map) screenshots may occur  
  → this can break the final video readability.
- Changing **Gameplay → “Map Icon Scale”** will affect the rendered video  
  unless everyone records using the **same setting**.
- During the first milliseconds after opening the map, UI elements (e.g. hardcap circles) animate in  
  → can affect the rendered result.
- Due to missing update/web/backend resources (to be delivered later), some functions were moved/changed  
  → see `VarGlobal.vb`.
- Because of the constraints above, some features currently require **debug commands** via `settings.ini`  
  (see below).

---

## Debug Commands

### Hardcoded (in code)
- `VarGlobal.vb` → Hardcoded values

### `settings.ini`
```ini
[internal_debug]
update_updater_timeout=1 (without update web service default: 1 | with update web service default: 0)
update_timeout=1 (without update web service default: 1 | with update web service default: 0)
timeout_between_save=5000 (default: 5000ms)
timeout_between_check=250 (default: 250ms)
