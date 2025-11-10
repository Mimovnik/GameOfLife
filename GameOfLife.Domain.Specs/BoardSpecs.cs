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

public class when_toggling_a_cell
{
    static Board board = null!;
    static Coords coords = new Coords(10, 10);

    Establish context = () =>
    {
        var dims = BoardDimensions.Create(100, 100);
        board = new Board(dims);
    };

    Because of = () => board.SetCellStateAt(coords, true);

    It board_is_updated = () => board.GetCellAt(coords).IsAlive.ShouldBeTrue();
}

