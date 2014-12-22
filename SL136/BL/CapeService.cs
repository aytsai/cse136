namespace Service
{
    using System;
    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class CapeService
    {
        private readonly ICapeRepository repository;

        public CapeService(ICapeRepository repository)
        {
            this.repository = repository;
        }

        public void InsertCape(Cape cape, ref List<string> errors)
        {
            if (cape == null)
            {
                errors.Add("Cape cannot be null");
                throw new ArgumentException();
            }

            this.repository.InsertCape(cape, ref errors);
        }

        public void UpdateCape(Cape cape, ref List<string> errors)
        {
            if (cape == null)
            {
                errors.Add("Cape cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateCape(cape, ref errors);
        }

        public void DeleteCape(int id, ref List<string> errors)
        {
            if (id <= 0)
            {
                errors.Add("Invalid cape id");
                throw new ArgumentException();
            }

            this.repository.DeleteCape(id, ref errors);
        }

        public Cape ViewCape(string id1, int id2, ref List<string> errors)
        {
            if (id1 == null)
            {
                errors.Add("Student can not be null");
                throw new ArgumentException();
            }

            return this.repository.ViewCape(id1, id2, ref errors);
        }

        public List<Cape> ViewStudentCape(int id, ref List<string> errors)
        {
            return this.repository.ViewStudentCape(id, ref errors);
        }
    }
}
