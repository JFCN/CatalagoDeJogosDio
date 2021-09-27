using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalago.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException()
            : base("Este jogo já está cadastrado")
        { }
    }
}
