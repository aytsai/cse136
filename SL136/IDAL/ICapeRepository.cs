namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface ICapeRepository
    {
        void InsertCape(Cape cape, ref List<string> errors);

        void UpdateCape(Cape cape, ref List<string> errors);

        void DeleteCape(int id, ref List<string> errors);

        Cape ViewCape(string id1, int id2, ref List<string> errors);

        List<Cape> ViewStudentCape(int id, ref List<string> errors);
    }
}
