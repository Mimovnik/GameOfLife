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

public class when_comparing_two_boards_with_same_dimensions_and_all_dead_cells
{
    static Board board1 = null!;
    static Board board2 = null!;
    static bool are_equal;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board1 = new Board(dimensions);
        board2 = new Board(dimensions);
    };

    Because of = () =>
        are_equal = board1.Equals(board2);

    It should_consider_them_equal = () =>
        are_equal.ShouldBeTrue();
}

public class when_comparing_two_boards_with_same_dimensions_and_same_alive_cells
{
    static Board board1 = null!;
    static Board board2 = null!;
    static bool are_equal;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board1 = new Board(dimensions);
        board2 = new Board(dimensions);
        
        board1.GetCellAt(new Coords(1, 1)).IsAlive = true;
        board1.GetCellAt(new Coords(2, 2)).IsAlive = true;
        
        board2.GetCellAt(new Coords(1, 1)).IsAlive = true;
        board2.GetCellAt(new Coords(2, 2)).IsAlive = true;
    };

    Because of = () =>
        are_equal = board1.Equals(board2);

    It should_consider_them_equal = () =>
        are_equal.ShouldBeTrue();
}

public class when_comparing_two_boards_with_same_dimensions_but_different_alive_cells
{
    static Board board1 = null!;
    static Board board2 = null!;
    static bool are_equal;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board1 = new Board(dimensions);
        board2 = new Board(dimensions);
        
        board1.GetCellAt(new Coords(1, 1)).IsAlive = true;
        board2.GetCellAt(new Coords(2, 2)).IsAlive = true;
    };

    Because of = () =>
        are_equal = board1.Equals(board2);

    It should_not_consider_them_equal = () =>
        are_equal.ShouldBeFalse();
}

public class when_comparing_two_boards_with_different_dimensions
{
    static Board board1 = null!;
    static Board board2 = null!;
    static bool are_equal;

    Establish context = () =>
    {
        var dimensions1 = BoardDimensions.Create(width: 3, height: 3);
        var dimensions2 = BoardDimensions.Create(width: 4, height: 4);
        board1 = new Board(dimensions1);
        board2 = new Board(dimensions2);
    };

    Because of = () =>
        are_equal = board1.Equals(board2);

    It should_not_consider_them_equal = () =>
        are_equal.ShouldBeFalse();
}

public class when_comparing_board_with_null
{
    static Board board = null!;
    static bool are_equal;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board = new Board(dimensions);
    };

    Because of = () =>
        are_equal = board.Equals(null);

    It should_not_be_equal = () =>
        are_equal.ShouldBeFalse();
}

public class when_comparing_board_with_itself
{
    static Board board = null!;
    static bool are_equal;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board = new Board(dimensions);
    };

    Because of = () =>
        are_equal = board.Equals(board);

    It should_be_equal = () =>
        are_equal.ShouldBeTrue();
}

public class when_getting_hash_code_for_two_equal_boards
{
    static Board board1 = null!;
    static Board board2 = null!;
    static int hash1;
    static int hash2;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board1 = new Board(dimensions);
        board2 = new Board(dimensions);
        
        board1.GetCellAt(new Coords(1, 1)).IsAlive = true;
        board2.GetCellAt(new Coords(1, 1)).IsAlive = true;
    };

    Because of = () =>
    {
        hash1 = board1.GetHashCode();
        hash2 = board2.GetHashCode();
    };

    It should_have_same_hash_codes = () =>
        hash1.ShouldEqual(hash2);
}

public class when_getting_hash_code_for_two_different_boards
{
    static Board board1 = null!;
    static Board board2 = null!;
    static int hash1;
    static int hash2;

    Establish context = () =>
    {
        var dimensions = BoardDimensions.Create(width: 3, height: 3);
        board1 = new Board(dimensions);
        board2 = new Board(dimensions);
        
        board1.GetCellAt(new Coords(1, 1)).IsAlive = true;
        board2.GetCellAt(new Coords(2, 2)).IsAlive = true;
    };

    Because of = () =>
    {
        hash1 = board1.GetHashCode();
        hash2 = board2.GetHashCode();
    };

    It should_have_different_hash_codes = () =>
        hash1.ShouldNotEqual(hash2);
}
