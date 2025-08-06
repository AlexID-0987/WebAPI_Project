namespace WebAPI_Project.Models
{
    public static class PetData
    {
        public static void Init(PetDbContext context)
        {
            if (!context.Pets.Any())
            {
                context.Pets.AddRange(
                    new Pet
                    {
                        Type="Dog",
                        Name="Lumpy"
                    },
                    new Pet
                    {
                        Type = "Cat",
                        Name = "Tom"
                    },
                    new Pet
                    {
                        Type = "Mouse",
                        Name = "Nike"
                    },
                    new Pet
                    {
                        Type = "Dog",
                        Name = "Frenky"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
