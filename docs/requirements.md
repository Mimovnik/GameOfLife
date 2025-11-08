# Game of Life Requirements (English)

## Core scope

- Build the application with Avalonia.
- Allow the user to define the board size (100×100 or larger), edit cells, clear, randomize, step the simulation once, run it continuously with speed control, and resume manual editing after pausing.
- Support saving and loading the automaton state (board + rules) from a text file.
- Display statistics such as: generation count, how many cells died, and how many were born.
- Let the user configure rules in the `B?/S?` notation (e.g., Conway’s Life is `B3/S23`, meaning births on 3 neighbors, survival on 2 or 3).
- Provide two zoom levels; when zoomed in, only part of the board is visible but the whole board continues updating.
- Offer simple visual customization of cells (color + shape options).
- Showcase common Avalonia concepts: styles, templates, triggers, animations, etc.

## Optional extensions

- Extremely large automatons (≥ 1000 × 1000).
- Multi-level or smooth zoom.
- Export the board to an image file, a sequence of images, or a video.
- Insert well-known patterns onto the current board (see https://pl.wikipedia.org/wiki/Gra_w_%C5%BCycie for inspiration).
- Implement colored Life variants such as Immigration Life or QuadLife.
- Use non-square grids (triangular, hexagonal, etc.).
- Support automatons with more than two states, e.g., Brian’s Brain or Wireworld (see https://robert.nowotniak.com/artificial-intelligence/GameOfLife/).
