using System.Diagnostics;


// Classe para nó da Árvore AVL
public class NoAVL
{
    public string Valor;
    public NoAVL Esquerda;
    public NoAVL Direita;
    public int Altura;
    
    public NoAVL(string valor)
    {
        Valor = valor;
        Esquerda = null;
        Direita = null;
        Altura = 1;
    }
}

// Classe para Árvore AVL
public class ArvoreAVL
{
    public NoAVL Raiz;
    public int ContadorInteracoes;
    public int ContadorBusca;
    
    public ArvoreAVL()
    {
        Raiz = null;
        ContadorInteracoes = 0;
        ContadorBusca = 0;
    }
    
    private int Altura(NoAVL no)
    {
        return no?.Altura ?? 0;
    }
    
    private int FatorBalanceamento(NoAVL no)
    {
        return no == null ? 0 : Altura(no.Esquerda) - Altura(no.Direita);
    }
    
    private NoAVL RotacaoDireita(NoAVL y)
    {
        NoAVL x = y.Esquerda;
        NoAVL T2 = x.Direita;
        
        x.Direita = y;
        y.Esquerda = T2;
        
        y.Altura = Math.Max(Altura(y.Esquerda), Altura(y.Direita)) + 1;
        x.Altura = Math.Max(Altura(x.Esquerda), Altura(x.Direita)) + 1;
        
        return x;
    }
    
    private NoAVL RotacaoEsquerda(NoAVL x)
    {
        NoAVL y = x.Direita;
        NoAVL T2 = y.Esquerda;
        
        y.Esquerda = x;
        x.Direita = T2;
        
        x.Altura = Math.Max(Altura(x.Esquerda), Altura(x.Direita)) + 1;
        y.Altura = Math.Max(Altura(y.Esquerda), Altura(y.Direita)) + 1;
        
        return y;
    }
    
    public NoAVL Inserir(NoAVL no, string valor)
    {
        if (no == null)
            return new NoAVL(valor);
        
        ContadorInteracoes++;
        if (string.Compare(valor, no.Valor) < 0)
            no.Esquerda = Inserir(no.Esquerda, valor);
        else if (string.Compare(valor, no.Valor) > 0)
            no.Direita = Inserir(no.Direita, valor);
        else
            return no;
        
        no.Altura = 1 + Math.Max(Altura(no.Esquerda), Altura(no.Direita));
        
        int balance = FatorBalanceamento(no);
        
        if (balance > 1 && string.Compare(valor, no.Esquerda.Valor) < 0)
            return RotacaoDireita(no);
        
        if (balance < -1 && string.Compare(valor, no.Direita.Valor) > 0)
            return RotacaoEsquerda(no);
        
        if (balance > 1 && string.Compare(valor, no.Esquerda.Valor) > 0)
        {
            no.Esquerda = RotacaoEsquerda(no.Esquerda);
            return RotacaoDireita(no);
        }
        
        if (balance < -1 && string.Compare(valor, no.Direita.Valor) < 0)
        {
            no.Direita = RotacaoDireita(no.Direita);
            return RotacaoEsquerda(no);
        }
        
        return no;
    }
    
    public bool Buscar(string valor)
    {
        ContadorBusca = 0;
        return BuscarRecursivo(Raiz, valor);
    }
    
    private bool BuscarRecursivo(NoAVL no, string valor)
    {
        if (no == null)
            return false;
        
        ContadorBusca++;
        
        if (string.Compare(valor, no.Valor) == 0)
            return true;
        else if (string.Compare(valor, no.Valor) < 0)
            return BuscarRecursivo(no.Esquerda, valor);
        else
            return BuscarRecursivo(no.Direita, valor);
    }
}

// Classe para nó da Árvore B
public class NoB
{
    public string[] Chaves;
    public NoB[] Filhos;
    public int NumChaves;
    public bool Folha;
    
    public NoB(bool folha)
    {
        Chaves = new string[5]; // Ordem 5 = máximo 4 chaves por nó
        Filhos = new NoB[6];   // Máximo 5 filhos
        NumChaves = 0;
        Folha = folha;
    }
}

// Classe para Árvore B
public class ArvoreB
{
    public NoB Raiz;
    public int ContadorInteracoes;
    public int ContadorBusca;
    
    public ArvoreB()
    {
        Raiz = null;
        ContadorInteracoes = 0;
        ContadorBusca = 0;
    }
    
    public void Inserir(string chave)
    {
        if (Raiz == null)
        {
            Raiz = new NoB(true);
            Raiz.Chaves[0] = chave;
            Raiz.NumChaves = 1;
        }
        else
        {
            if (Raiz.NumChaves == 4)
            {
                NoB novaRaiz = new NoB(false);
                novaRaiz.Filhos[0] = Raiz;
                DividirFilho(novaRaiz, 0);
                
                int i = 0;
                if (string.Compare(chave, novaRaiz.Chaves[0]) > 0)
                    i++;
                
                InserirNaoCheio(novaRaiz.Filhos[i], chave);
                Raiz = novaRaiz;
            }
            else
            {
                InserirNaoCheio(Raiz, chave);
            }
        }
    }
    
    private void InserirNaoCheio(NoB no, string chave)
    {
        int i = no.NumChaves - 1;
        
        if (no.Folha)
        {
            while (i >= 0 && string.Compare(chave, no.Chaves[i]) < 0)
            {
                no.Chaves[i + 1] = no.Chaves[i];
                i--;
            }
            
            no.Chaves[i + 1] = chave;
            no.NumChaves++;
        }
        else
        {
            while (i >= 0 && string.Compare(chave, no.Chaves[i]) < 0)
                i--;
            
            i++;
            
            if (no.Filhos[i].NumChaves == 4)
            {
                DividirFilho(no, i);
                
                if (string.Compare(chave, no.Chaves[i]) > 0)
                    i++;
            }
            
            InserirNaoCheio(no.Filhos[i], chave);
        }
    }
    
    private void DividirFilho(NoB pai, int i)
    {
        NoB no = pai.Filhos[i];
        NoB novoNo = new NoB(no.Folha);
        
        novoNo.NumChaves = 2;
        
        for (int j = 0; j < 2; j++)
            novoNo.Chaves[j] = no.Chaves[j + 2];
        
        if (!no.Folha)
        {
            for (int j = 0; j < 3; j++)
                novoNo.Filhos[j] = no.Filhos[j + 2];
        }
        
        no.NumChaves = 2;
        
        for (int j = pai.NumChaves; j >= i + 1; j--)
            pai.Filhos[j + 1] = pai.Filhos[j];
        
        pai.Filhos[i + 1] = novoNo;
        
        for (int j = pai.NumChaves - 1; j >= i; j--)
            pai.Chaves[j + 1] = pai.Chaves[j];
        
        pai.Chaves[i] = no.Chaves[2];
        pai.NumChaves++;
    }
    
    public bool Buscar(string chave)
    {
        ContadorBusca = 0;
        return BuscarRecursivo(Raiz, chave);
    }
    
    private bool BuscarRecursivo(NoB no, string chave)
    {
        if (no == null)
            return false;
        
        int i = 0;
        while (i < no.NumChaves && string.Compare(chave, no.Chaves[i]) > 0)
        {
            ContadorBusca++;
            i++;
        }
        
        ContadorBusca++;
        
        if (i < no.NumChaves && string.Compare(chave, no.Chaves[i]) == 0)
            return true;
        
        if (no.Folha)
            return false;
        
        return BuscarRecursivo(no.Filhos[i], chave);
    }
}

// Programa principal
Console.WriteLine("=== INÍCIO DO PROCESSO ===");
Console.WriteLine("Gerando 1.000.000 strings únicas (5-7 caracteres)...");

var stopwatch = Stopwatch.StartNew();

// Gerar 50.000 strings únicas
var strings = GerarStringsUnicas(1000000).ToList();

Console.WriteLine($"Strings geradas em {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine("\n=== INSERINDO NAS ESTRUTURAS ===");

// Estrutura 1: List<string>
stopwatch.Restart();
var lista = new List<string>();
var contadorLista = 0;
foreach (var str in strings)
{
    lista.Add(str);
}
var tempoInsercaoLista = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Lista: Inserção concluída em {tempoInsercaoLista} ms");

// Estrutura 2: Árvore AVL
stopwatch.Restart();
var arvoreAVL = new ArvoreAVL();
foreach (var str in strings)
{
    arvoreAVL.Raiz = arvoreAVL.Inserir(arvoreAVL.Raiz, str);
}
var tempoInsercaoAVL = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Árvore AVL: Inserção concluída em {tempoInsercaoAVL} ms");

// Estrutura 3: Árvore B
stopwatch.Restart();
var arvoreB = new ArvoreB();
foreach (var str in strings)
{
    arvoreB.Inserir(str);
}
var tempoInsercaoB = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"Árvore B: Inserção concluída em {tempoInsercaoB} ms");

Console.WriteLine("\n=== TESTES DE BUSCA ===");

// Selecionar 3 strings para busca
var stringsBusca = new string[] { strings[100000], strings[250000], strings[750000] };

foreach (var busca in stringsBusca)
{
    Console.WriteLine($"\nBuscando: '{busca}'");
    
    // Busca na Lista
    stopwatch.Restart();
    contadorLista = 0;
    bool encontradoLista = false;
    foreach (var item in lista)
    {
        contadorLista++;
        if (item == busca)
        {
            encontradoLista = true;
            break;
        }
    }
    var tempoBuscaLista = stopwatch.Elapsed.TotalMilliseconds;
    Console.WriteLine($"Lista: Encontrado={encontradoLista}, Tempo={tempoBuscaLista:F6} ms, Comparações={contadorLista}");
    
    // Busca na Árvore AVL
    stopwatch.Restart();
    bool encontradoAVL = arvoreAVL.Buscar(busca);
    var tempoBuscaAVL = stopwatch.Elapsed.TotalMilliseconds;
    Console.WriteLine($"Árvore AVL: Encontrado={encontradoAVL}, Tempo={tempoBuscaAVL:F6} ms, Comparações={arvoreAVL.ContadorBusca}");
    
    // Busca na Árvore B
    stopwatch.Restart();
    bool encontradoB = arvoreB.Buscar(busca);
    var tempoBuscaB = stopwatch.Elapsed.TotalMilliseconds;
    Console.WriteLine($"Árvore B: Encontrado={encontradoB}, Tempo={tempoBuscaB:F6} ms, Comparações={arvoreB.ContadorBusca}");
}

Console.WriteLine("\n=== PROCESSO CONCLUÍDO ===");

private static HashSet<string> GerarStringsUnicas(int quantidade)
{
    var random = new Random();
    var strings = new HashSet<string>();
    const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    while (strings.Count < quantidade)
    {
        int tamanho = random.Next(5, 8); // 5, 6 ou 7 caracteres
        var str = new string(Enumerable.Repeat(chars, tamanho)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        strings.Add(str);
    }
    
    return strings;
}
