using Machine.Specifications;

namespace GameOfLife.Domain.Specs;

public class when_creating_board_dimensions_with_width_zero
{
    static Exception exception = null!;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: 0, height: 120));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_creating_board_dimensions_with_negative_width
{
    static Exception exception = null!;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: -5, height: 120));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_creating_board_dimensions_with_height_zero
{
    static Exception exception = null!;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: 120, height: 0));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_creating_board_dimensions_with_negative_height
{
    static Exception exception = null!;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: 120, height: -80));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_comparing_two_board_dimensions_with_same_values
{
    static BoardDimensions dimensions1 = null!;
    static BoardDimensions dimensions2 = null!;
    static bool are_equal;

    Establish context = () =>
    {
        dimensions1 = BoardDimensions.Create(width: 100, height: 200);
        dimensions2 = BoardDimensions.Create(width: 100, height: 200);
    };

    Because of = () =>
        are_equal = dimensions1.Equals(dimensions2);

    It should_consider_them_equal = () =>
        are_equal.ShouldBeTrue();
}

public class when_getting_all_cells_row_by_row
{
    static Board board = null!;
    static List<Cell> cells = null!;

    Establish context = () =>
    {
        var dims = BoardDimensions.Create(3, 2);
        board = new Board(dims);
        board.Cells[0, 0] = new Cell(isAlive: false);
        board.Cells[1, 0] = new Cell(isAlive: true);
        board.Cells[2, 0] = new Cell(isAlive: false);
        board.Cells[0, 1] = new Cell(isAlive: true);
        board.Cells[1, 1] = new Cell(isAlive: false);
        board.Cells[2, 1] = new Cell(isAlive: true);
    };

    Because of = () =>
        cells = board.GetCellsRowByRow().ToList();

    It should_return_cells_in_row_order = () =>
    {
        cells[0].IsAlive.ShouldBeFalse();
        cells[1].IsAlive.ShouldBeTrue();
        cells[2].IsAlive.ShouldBeFalse();
        cells[3].IsAlive.ShouldBeTrue();
        cells[4].IsAlive.ShouldBeFalse();
        cells[5].IsAlive.ShouldBeTrue();
    };
}

public class when_toggling_a_cell
{
    static Board board;
    static Cell result;

    Establish context = () =>
    {
        var dims = BoardDimensions.Create(100, 100);
        board = new Board(dims);
        board.Cells[10, 10] = new Cell(isAlive: false);
    };

    Because of = () => result = board.ChangeCellStateAt(new Coords(10, 10));

    It should_mark_cell_alive = () => result.IsAlive.ShouldBeTrue();
}

