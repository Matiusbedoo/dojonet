using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dojonet.Modelo;
using Google.Cloud.Firestore;

namespace dojonet
{
    public class FirebaseAccount
    {   

        private readonly static FirebaseAccount _instance = new FirebaseAccount();
        FirestoreDb _db;
        Usuario user= new Usuario();
        public FirebaseAccount() {
            String path = AppDomain.CurrentDomain.BaseDirectory + @"Firebase-SDK.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",path);
            _db = FirestoreDb.Create("dojo-net-core");
            Console.WriteLine("sucessful");
        }
         public static FirebaseAccount Instance
        {
            get
            {
                return _instance;
            }
        }

        public async Task<String> AddUser(Usuario user)
        {
            DocumentReference coll = _db.Collection("Usuarios").Document();
            Dictionary <String,object> data = new Dictionary<string, object>()
            {
                {"Cedula",user.Cedula},
                {"Nombre",user.Nombre},
                {"Correo",user.Correo},
                {"Carrera",user.Carrera}
            };
            await coll.SetAsync(data);
            return "Usuario guardado con Id:"+ coll.Id;
        }

        public async Task<List<Usuario>> GetUser()
        {
            CollectionReference userRef= _db.Collection("Usuarios");
            QuerySnapshot queryUser = await userRef.GetSnapshotAsync();
            List<Usuario> userlist = new List<Usuario>();
            foreach (DocumentSnapshot documenSnapshot in queryUser.Documents)
            {
                Dictionary <String,Object>  usuario = documentSnapshot.ToDictionary();
                Usuario user = new Usuario();
                foreach (var item in usuario)
                {
                    if (item.Key == "Nombre")
                    {
                        user.Nombre=(String) item.value;
                    }else if (item.key=="Cedula")
                    {
                        user.Cedula=(String) item.value;
                    }else if (item.key=="Correo")
                    {
                        user.Correo=(String) item.value;
                    }else if (item.key=="Carrera")
                    {
                        user.Carrera=(String) item.value;
                    }
                }
                userlist.Add(user);
            }
            return userList;
        }

        public async Task<String> DeleteUser (String Id)
        {
            DocumentReference userDelete =  _db.Collection("Usuarios").Document(id);
            await userDelete.DeleteAsync();
            return "Usuario con Id" +id+ "eliminado correctamente";
        }
    }
}