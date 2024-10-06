using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Task = Box.Sdk.Gen.Schemas.Task;

namespace BoxDrive;

public class prueba
{
    private readonly BoxJWTAuth _boxJwtAuth;
    private readonly string _adminToken;
    private readonly BoxClient _adminClient;

    public prueba()
    {
        var reader = new StreamReader("config/text.json");
        var json = reader.ReadToEnd();
        var config = BoxConfigBuilder.CreateFromJsonString(json).Build();
        // Cargar la configuración desde el archivo JSON
        _boxJwtAuth = new BoxJWTAuth(config);
        
        // Generar token de administrador
        _adminToken = _boxJwtAuth.AdminTokenAsync().Result;  // ¡Mejor usar async/await en tu implementación!
        _adminClient = _boxJwtAuth.AdminClient(_adminToken);
    }
    
    public async Task<BoxClient> GetAdminClientAsync()
    {
        var token = await _boxJwtAuth.AdminTokenAsync();
        return _boxJwtAuth.AdminClient(token);
    }

    public async Task<BoxClient> GetAppUserClientAsync(string appUserId)
    {
        var userToken = await _boxJwtAuth.UserTokenAsync(appUserId);
        return _boxJwtAuth.UserClient(userToken, appUserId);
    }
    
}