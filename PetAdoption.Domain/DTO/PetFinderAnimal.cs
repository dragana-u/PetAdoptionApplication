namespace PetAdoption.Domain.DTO;

public class PetfinderAnimalSearchResponse
{
    public List<PetfinderAnimal> animals { get; set; } = new();
}

public class PetfinderAnimal
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? gender { get; set; }
    public string? size { get; set; }
    public string? age { get; set; }
    public string? status { get; set; }
    public BreedInfo breeds { get; set; } = new();
    public string? type { get; set; }
    public Photo[] photos { get; set; } = Array.Empty<Photo>();
    public string? published_at { get; set; }
}

public class BreedInfo
{
    public string? primary { get; set; }
}

public class Photo
{
    public string? medium { get; set; }
}

public class PetfinderTokenResponse
{
    public string access_token { get; set; } = default!;
    public int expires_in { get; set; }
    public string token_type { get; set; } = default!;
}
