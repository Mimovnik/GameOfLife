# Implementation Plan (Vertical Slices + TDD)

Follow one requirement at a time. For each slice work bottom-up (Domain → Application → Infrastructure → Presentation) and use TDD in the Domain phase: write/extend specs, watch them fail, implement minimal code, then refactor.

## Slice 1: Board size & editing basics
1. **Domain (TDD)**: Specs for `BoardDimensions`, `CellState`, editing commands (set alive/dead, clear, randomize seed stub). Implement immutable `BoardState`, neighbor helpers, validation for ≥100×100, then refactor.
2. **Application**: `IBoardEditorService` handling size changes, edit mode, clear/randomize operations, returning result objects for validation errors.
3. **Infrastructure**: Deterministic RNG adapter + persistence abstractions needed later (stub implementations for now).
4. **Presentation**: Initial ViewModels for board editing UI (size picker, edit toggle). Bind commands to `IBoardEditorService`, render interactive grid.

## Slice 2: Simulation stepping (single + continuous with pause/resume)
1. **Domain (TDD)**: Specs for `RuleSet` (`B?/S?`) parsing/validation and `SimulationEngine.Step`. Add `SimulationTimeline` tracking generations and edit/run transitions.
2. **Application**: `ISimulationController` to run single steps or continuous loop with adjustable speed, enforcing pause → edit mode transitions.
3. **Infrastructure**: Timer/dispatcher adapter for scheduling ticks, cancellation support to keep UI responsive.
4. **Presentation**: Bind Step/Play/Pause commands, speed slider, and indicators for current mode. Ensure animation stops before editing resumes.

## Slice 3: Load/Save state (board + rules)
1. **Domain (TDD)**: Specs for text serialization DTOs covering cells + rules; implement `BoardSnapshotSerializer` with edge-case tests.
2. **Application**: `IStatePersistenceService` coordinating save/load and swapping state within the simulation controller.
3. **Infrastructure**: File I/O implementations, safe-write handling, error propagation.
4. **Presentation**: Save/Load commands triggering file dialogs; show success/error feedback.

## Slice 4: Statistics (generation count, births, deaths)
1. **Domain (TDD)**: Specs for `StatisticsTracker` computing metrics per step; integrate with simulation results.
2. **Application**: Extend controller/service to emit statistics DTOs and queries.
3. **Infrastructure**: Optional logging/export adapters (e.g., append stats to file).
4. **Presentation**: Bind stats to UI elements (labels/charts) updating alongside simulation.

## Slice 5: Rule configuration UI (`B?/S?`)
1. **Domain (TDD)**: Extend specs for custom rule validation (allowed neighbors, duplicates).  
2. **Application**: Commands to validate/apply rule changes safely while running.  
3. **Infrastructure**: None unless persisting presets.  
4. **Presentation**: Rule editor UI with immediate validation feedback and default Conway preset.

## Slice 6: Dual-level zoom
1. **Domain (TDD)**: Introduce `Viewport` value object; specs for translating board coordinates to visible region.  
2. **Application**: Service managing zoom levels while keeping full board simulation running.  
3. **Infrastructure**: Optional persistence of zoom preference.  
4. **Presentation**: Zoom toggle controls; render partial view but continue updating entire board.

## Slice 7: Cell appearance customization + Avalonia styling showcase
1. **Domain (TDD)**: Config objects/enums describing allowed colors/shapes per state.  
2. **Application**: Expose appearance selections, command to switch styles.  
3. **Infrastructure**: Resource provider loading palettes/shapes from config if needed.  
4. **Presentation**: Avalonia styles/templates/triggers/animations bound to ViewModel props to fulfill styling requirement.

## Slice 8: Optional feature backlog
- For each optional requirement (huge boards, multi-level zoom, exports, pattern inserter, colored variants, alternative grids, multi-state automata), repeat the same vertical-slice approach: Domain specs first, then ripple through Application, Infrastructure, and Presentation.
