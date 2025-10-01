using server.Core.Entities;
using Microsoft.AspNetCore.Mvc;

public class FilesPacket
{
    public List<IFormFile> Files { get; set; }
    public string SessionName { get; set; }
}