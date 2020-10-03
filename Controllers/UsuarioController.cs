using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dojonet.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace dojonet.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        FirebaseAccount b = FirebaseAccount.Instance;

        Usuario usuario= new Usuario();

        

        [HttpGet]
        public async Task<List<Usuario>> Get()
        {
            FirebaseAccount Instancia =FirebaseAccount.Instancia;
            return await Instancia.GetUser();    
        }

        [HttpPost]
        public async Task<String> Post(Usuario user)
        {
            return await Instancia.AddUser(user);    
        }

        [HttpDelete]
        public async Task<String> Delete(String id)
        {
            return await Instancia.DeleteUser(id);    
        }
        
    }
}