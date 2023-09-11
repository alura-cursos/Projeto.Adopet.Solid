﻿using Alura.Adopet.Console.Servicos.Abstracoes;
using Alura.Adopet.Console.Settings;
using System.Net.Mail;
using System.Net;
using FluentResults;
using Alura.Adopet.Console.Results;

namespace Alura.Adopet.Console.Servicos.Mail;

public class EnvioDeEmail
{
    private IMailService CriarMailService()
    {
        AppSettings settings = Configurations.GetSettings();
        SmtpClient smtpClient = new()
        {
            Host = settings.Servidor!,
            Port = settings.Porta,
            Credentials = new NetworkCredential(settings.Usuario, settings.Senha),
            EnableSsl = true,
            UseDefaultCredentials = false
        };
        return new SmtpClientMailService(smtpClient);
    }

    public void Disparar(Result resultado)
    {
        ISuccess? success = resultado.Successes.FirstOrDefault();
        if (success is null) return;

        if (success is SuccessWithPets sucesso)
        {
            var emailService = CriarMailService();
            emailService.SendMailAsync(
                remetente: "no-reply@adopet.com.br",
                titulo: $"[Adopet] {sucesso.Message}",
                corpo: $"Foram importados {sucesso.Data.Count()} pets.",
                destinatario: "andrebessax@gmail.com"
            );
        }
    }
}
