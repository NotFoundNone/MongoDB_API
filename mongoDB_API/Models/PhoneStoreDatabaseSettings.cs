namespace mongoDB_API.Models;

public class PhoneStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PhonesCollectionName { get; set; } = null!;
}