using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionaCareca.Shared.Models
{
    public class Camera
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string DiretorioImagens { get; set; } = string.Empty;
        public bool Ativa { get; set; } = true;

    }
}
