using System;
using System.Threading.Tasks;
using Box.V2.Models;
using Microsoft.AspNetCore.Mvc;
using Box.Sdk.Gen;

namespace BoxDrive.Controllers;

[Route("[controller]")]
[ApiController]
public class textController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> hola()
    {
        // Crear instancia del servicio Box
        var boxService = new prueba();

        // Obtener cliente de administrador
        var adminClient = await boxService.GetAdminClientAsync();

        string folderId = "0"; // Cambia esto por el ID de la carpeta que quieres ver
        
        BoxFolder folder = await adminClient.FoldersManager.GetInformationAsync(folderId);

        // Obtener los elementos dentro del folder
        BoxCollection<BoxItem> items = await adminClient.FoldersManager.GetFolderItemsAsync(folderId, limit: 100, offset: 0);

        // Mostrar la información de la carpeta
        Console.WriteLine($"Carpeta: {folder.Name} (ID: {folder.Id})");

        return Ok(items);
    }

    [HttpGet("holi")]
    public async Task<ActionResult> holi()
    {
        

        var auth = new BoxDeveloperTokenAuth(token: "gCEJ0FQLm0z4n44A6Ko2RPA3K76WIaw4");
        var client = new BoxClient(auth: auth);

        var items = await client.Folders.GetFolderItemsAsync(folderId: "0");
        if (items.Entries != null)
        {
            foreach (var item in items.Entries)
            {
                if (item.FileFull != null)
                {
                    Console.WriteLine(item.FileFull.Name);
                }
                else if (item.FolderMini != null)
                {
                    Console.WriteLine(item.FolderMini.Name);
                }
                else if (item.WebLink != null)
                {
                    Console.WriteLine(item.WebLink.Name);
                }
            }
        }
        
        return Ok(items);
    }

    [HttpGet("ulu")]
    public async Task<ActionResult> holi2()
    {
        var config = JwtConfig.FromConfigFile("config/text.json");
        var jwtAuth = new BoxJwtAuth(config);
        var client = new BoxClient(jwtAuth);

        var me = await client.Users.GetUserMeAsync();
        Console.WriteLine($"My user ID is {me.Id}");
        
        var items = await client.Folders.GetFolderItemsAsync(folderId: "0");
        if (items.Entries != null)
        {
            foreach (var item in items.Entries)
            {
                if (item.FileFull != null)
                {
                    Console.WriteLine(item.FileFull.Name);
                }
                else if (item.FolderMini != null)
                {
                    Console.WriteLine(item.FolderMini.Name);
                }
                else if (item.WebLink != null)
                {
                    Console.WriteLine(item.WebLink.Name);
                }
            }
        }
        
        return Ok(items);

    }
}