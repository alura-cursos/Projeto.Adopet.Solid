﻿using Alura.Adopet.Console.Modelos;
using Alura.Adopet.Console.Servicos.Abstracoes;
using System.Text.Json;

namespace Alura.Adopet.Console.Servicos.Arquivos;

public class LeitorDeArquivoJson<T> : ILeitorDeArquivo<T>
{
    private readonly string caminhoArquivoASerLido;

    public LeitorDeArquivoJson(string caminhoArquivoASerLido)
    {
        this.caminhoArquivoASerLido = caminhoArquivoASerLido;
    }

    public IEnumerable<T> RealizaLeitura()
    {
        using var stream = new FileStream(caminhoArquivoASerLido, FileMode.Open, FileAccess.Read);
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<IEnumerable<T>>(stream, options) ?? throw new ArgumentException($"Erro na deserialização do arquivo {caminhoArquivoASerLido}");
    }
}
