using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_MODERNISTA
{
    class datos
    {
    }
    public class usuarios {
        int id_usuario;
        string nombre;
        string apellido;
        string usuario;
        string contraseña;
        string tipo_usuario;

        

        public usuarios(int id_usuario, string nombre, string apellido, string usuario, string contraseña, string tipo_usuario)
        {
            this.id_usuario = id_usuario;
            this.nombre = nombre;
            this.apellido = apellido;
            this.usuario = usuario;
            this.contraseña = contraseña;
            this.tipo_usuario = tipo_usuario;
        }

        public int Id_usuario { get => id_usuario; set => id_usuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Tipo_usuario { get => tipo_usuario; set => tipo_usuario = value; }
    }
}

