using System.Collections.Generic;

namespace ApAppen
{
    public record Category(string Name, IEnumerable<Deduction> Deductions)
    {
        public static IEnumerable<Category> Categories = new[]
        {
            new Category(
                "Pole singles technical deduction",
                new[]
                {
                    new Deduction("Poor execution and incorrect lines", -0.2m),
                    new Deduction("Poor transitions in and out of elements and on and off the pole", -0.5m),
                    new Deduction("Poor presentation of the element", -0.5m),
                    new Deduction("A slip of loss of balance", -1m),
                    new Deduction("Touching the rigging or truss system during the performance", -1m),
                    new Deduction("A Fall", -3m)
                }),
            new Category(
                "Pole doubles technical deduction",
                new[]
                {
                    new Deduction("Poor execution and incorrect lines", -0.2m),
                    new Deduction("Lack of synchronicity of element", -0.5m),
                    new Deduction("Poor transitions in and out of elements and on and off the pole", -0.5m),
                    new Deduction("Poor presentation of the element", -0.5m),
                    new Deduction("A slip of loss of balance", -1m),
                    new Deduction("Touching the rigging or truss system during the performance", -1m),
                    new Deduction("A Fall", -3m)
                }),
            new Category(
                "Aerial singles technical deduction",
                new[]
                {
                    new Deduction("Poor execution and incorrect lines", -0.2m),
                    new Deduction("Poor transitions in and out of elements and on and off the hoop", -0.5m),
                    new Deduction("Poor presentation of the element", -0.5m),
                    new Deduction("A slip or loss of balance", -1m),
                    new Deduction("Touching the truss system during the performance", -1m),
                    new Deduction("A Fall", -3m)
                }),
            new Category(
                "Aerial doubles technical deduction",
                new[]
                {
                    new Deduction("Poor execution and incorrect lines", -0.2m),
                    new Deduction("Lack of synchronicity of element", -0.5m),
                    new Deduction("Poor transitions in and out of elements and on and off the hoop", -0.5m),
                    new Deduction("Poor presentation of the element", -0.5m),
                    new Deduction("A slip or loss of balance", -1m),
                    new Deduction("Touching the truss system during the performance", -1m),
                    new Deduction("A Fall", -3m)
                })
        };
    }

    public record Deduction(string Name, decimal Points);
}
