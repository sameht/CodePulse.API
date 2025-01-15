using System;

namespace CodePulse.API.Models.DTO;

public class UpdateCategoryResquestDto
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UrlHandle { get; set; }
}

