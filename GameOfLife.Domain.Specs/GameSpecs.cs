using Machine.Specifications;

namespace GameOfLife.Domain.Specs;

public class when_applying_birth_condition_with_three_neighbors
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static Coords centerCoords = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(3, 3);
        currentBoard = new Board(dimensions);
        centerCoords = new Coords(1, 1);
        
        currentBoard.GetCellAt(new Coords(0, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(1, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 0)).IsAlive = true;
        currentBoard.GetCellAt(centerCoords).IsAlive = false;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_bring_the_dead_cell_to_life = () =>
        nextBoard.GetCellAt(centerCoords).IsAlive.ShouldBeTrue();
}

public class when_applying_birth_condition_with_insufficient_neighbors
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static Coords centerCoords = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(3, 3);
        currentBoard = new Board(dimensions);
        centerCoords = new Coords(1, 1);
        
        currentBoard.GetCellAt(new Coords(0, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(1, 0)).IsAlive = true;
        currentBoard.GetCellAt(centerCoords).IsAlive = false;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_keep_the_cell_dead = () =>
        nextBoard.GetCellAt(centerCoords).IsAlive.ShouldBeFalse();
}

public class when_applying_survival_condition_with_two_neighbors
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static Coords centerCoords = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(3, 3);
        currentBoard = new Board(dimensions);
        centerCoords = new Coords(1, 1);
        
        currentBoard.GetCellAt(centerCoords).IsAlive = true;
        currentBoard.GetCellAt(new Coords(0, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 0)).IsAlive = true;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_keep_the_cell_alive = () =>
        nextBoard.GetCellAt(centerCoords).IsAlive.ShouldBeTrue();
}

public class when_applying_survival_condition_with_three_neighbors
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static Coords centerCoords = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(3, 3);
        currentBoard = new Board(dimensions);
        centerCoords = new Coords(1, 1);
        
        currentBoard.GetCellAt(centerCoords).IsAlive = true;
        currentBoard.GetCellAt(new Coords(0, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(1, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 0)).IsAlive = true;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_keep_the_cell_alive = () =>
        nextBoard.GetCellAt(centerCoords).IsAlive.ShouldBeTrue();
}

public class when_alive_cell_has_too_few_neighbors
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static Coords centerCoords = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(3, 3);
        currentBoard = new Board(dimensions);
        centerCoords = new Coords(1, 1);
        
        currentBoard.GetCellAt(centerCoords).IsAlive = true;
        currentBoard.GetCellAt(new Coords(0, 0)).IsAlive = true;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_die_from_underpopulation = () =>
        nextBoard.GetCellAt(centerCoords).IsAlive.ShouldBeFalse();
}

public class when_alive_cell_has_too_many_neighbors
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static Coords centerCoords = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(3, 3);
        currentBoard = new Board(dimensions);
        centerCoords = new Coords(1, 1);
        
        currentBoard.GetCellAt(centerCoords).IsAlive = true;
        currentBoard.GetCellAt(new Coords(0, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(1, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 0)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(0, 1)).IsAlive = true;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_die_from_overpopulation = () =>
        nextBoard.GetCellAt(centerCoords).IsAlive.ShouldBeFalse();
}

public class when_processing_a_blinker_pattern
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(5, 5);
        currentBoard = new Board(dimensions);
        
        currentBoard.GetCellAt(new Coords(2, 1)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 2)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 3)).IsAlive = true;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_rotate_to_horizontal_orientation = () =>
    {
        nextBoard.GetCellAt(new Coords(2, 1)).IsAlive.ShouldBeFalse();
        nextBoard.GetCellAt(new Coords(2, 3)).IsAlive.ShouldBeFalse();
        
        nextBoard.GetCellAt(new Coords(1, 2)).IsAlive.ShouldBeTrue();
        nextBoard.GetCellAt(new Coords(2, 2)).IsAlive.ShouldBeTrue();
        nextBoard.GetCellAt(new Coords(3, 2)).IsAlive.ShouldBeTrue();
    };
}

public class when_processing_a_block_pattern
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        
        var dimensions = BoardDimensions.Create(4, 4);
        currentBoard = new Board(dimensions);
        
        currentBoard.GetCellAt(new Coords(1, 1)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 1)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(1, 2)).IsAlive = true;
        currentBoard.GetCellAt(new Coords(2, 2)).IsAlive = true;
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_remain_stable = () =>
    {
        nextBoard.GetCellAt(new Coords(1, 1)).IsAlive.ShouldBeTrue();
        nextBoard.GetCellAt(new Coords(2, 1)).IsAlive.ShouldBeTrue();
        nextBoard.GetCellAt(new Coords(1, 2)).IsAlive.ShouldBeTrue();
        nextBoard.GetCellAt(new Coords(2, 2)).IsAlive.ShouldBeTrue();
    };
}

public class when_next_generation_preserves_board_dimensions
{
    static RuleSet ruleSet = null!;
    static Board currentBoard = null!;
    static Board nextBoard = null!;
    static BoardDimensions originalDimensions = null!;

    Establish context = () =>
    {
        ruleSet = new RuleSet(birthConditions: new[] { 3 }, survivalConditions: new[] { 2, 3 });
        originalDimensions = BoardDimensions.Create(10, 15);
        currentBoard = new Board(originalDimensions);
    };

    Because of = () =>
        nextBoard = ruleSet.NextGeneration(currentBoard);

    It should_have_same_dimensions = () =>
        nextBoard.Dimensions.ShouldEqual(originalDimensions);
}
