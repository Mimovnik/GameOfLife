using Machine.Specifications;

namespace GameOfLife.Domain.Specs;

public class when_creating_board_dimensions_with_width_zero
{
    static Exception exception;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: 0, height: 120));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_creating_board_dimensions_with_negative_width
{
    static Exception exception;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: -5, height: 120));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_creating_board_dimensions_with_height_zero
{
    static Exception exception;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: 120, height: 0));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}

public class when_creating_board_dimensions_with_negative_height
{
    static Exception exception;

    Because of = () =>
        exception = Catch.Exception(() => BoardDimensions.Create(width: 120, height: -80));

    It should_throw_an_out_of_range_error = () =>
        exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
}
