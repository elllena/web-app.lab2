using FluentValidation;

namespace Lab2.Web.Resources.CreateKnife
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Knife).NotNull();

            RuleFor(x => x.Knife.Name).NotEmpty();
            RuleFor(x => x.Knife.Origin).NotEmpty();


            RuleFor(x => x.Knife.Visual).NotNull();
            RuleFor(x => x.Knife.Visual.Height).GreaterThan(0);
            RuleFor(x => x.Knife.Visual.Width).GreaterThan(0);
            RuleFor(x => x.Knife.Visual.HandType).NotNull();



            RuleFor(x => x.Knife.Visual.HandType.WoodType)
                .Null().When(x => x.Knife.Visual.HandType.Material != Entities.HandMaterial.Wood);

            RuleFor(x => x.Knife.Visual.HandType.WoodType)
                .NotNull().When(x => x.Knife.Visual.HandType.Material == Entities.HandMaterial.Wood);

        }
    }
}
