using System;

public class ClasseFilha : ClasseBase
{
    public void MetodoFilha()
    {
        // Acesso ao membro protegido da classe base
        Id = 10;
    }
}
