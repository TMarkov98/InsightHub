namespace InsightHub.Services.Contracts
{
    public interface IEmailSenderServices
    {
        void AutoSendMail(string to);
    }
}