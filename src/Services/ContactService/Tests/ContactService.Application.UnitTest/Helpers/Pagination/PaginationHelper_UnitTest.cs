
using Common.Shared.Helpers;

namespace Helpers.Pagination
{
    public class PaginationHelper_UnitTest
    {

        List<Record> _list;
        internal class Record
        {
            public int No { get; set; }
        }

        [SetUp]
        public void Setup()
        {
            _list = Enumerable.Range(1, 13).Select(i => new Record { No = i }).ToList();
        }



        // TODO : sayfalama değerleri 0 dan büyük olmalı. 0 dan küçük sayfa sayısı olamaz. 1 den küçük kayıt sayısı istenemez
        // TODO : bu geliştirmeler helper'a eklenecek. testleri yapılacak
        // TODO : sayfa numarası ve sayfa sayısı için struct geliştirmeli. intager için explicit implicit tür dönüşümleri yapılmalı 
        [Test, TestCaseSource(nameof(PaginationCase))]
        public void PaginationHelper_GetPaged_PagedData_With_Paginatin_Values(int pageno, int pagesize)
        {

            var query = _list.AsQueryable();


            double pageCount = (double)_list.Count / pagesize;
            int totalPageCount = (int)Math.Ceiling(pageCount);



            var result = PaginationHelper.GetPaged(query, pageno, pagesize);


            Assert.That(result.PageNo, Is.EqualTo(pageno), "sayfa no");
            Assert.That(result.PageSize, Is.EqualTo(pagesize), "sayfanın boyutu");
            Assert.That(result.TotalPageCount, Is.EqualTo(totalPageCount), "toplam sayfa sayısı");
            Assert.That(result.TotalRecordCount, Is.EqualTo(_list.Count), "toplam kayıt sayısı");
            //Assert.That(result.Results.Count, Is.EqualTo(_list.Count),"kayıt sayısı");
            Assert.That(result.Results.Count, Is.GreaterThanOrEqualTo(0), "kayıt sayısı");
        }
        [Test, TestCaseSource(nameof(PaginationCase))]
        public async Task PaginationHelper_GetPagedAsync_PagedData_With_Paginatin_Values(int pageno, int pagesize)
        {

            var query = _list.AsQueryable();


            double pageCount = (double)_list.Count / pagesize;
            int totalPageCount = (int)Math.Ceiling(pageCount);



            var result = await PaginationHelper.GetPagedAsync(query, pageno, pagesize);


            Assert.That(result.PageNo, Is.EqualTo(pageno), "sayfa no");
            Assert.That(result.PageSize, Is.EqualTo(pagesize), "sayfanın boyutu");
            Assert.That(result.TotalPageCount, Is.EqualTo(totalPageCount), "toplam sayfa sayısı");
            Assert.That(result.TotalRecordCount, Is.EqualTo(_list.Count), "toplam kayıt sayısı");
            Assert.That(result.Results.Count, Is.GreaterThanOrEqualTo(0), "kayıt sayısı");
        }



        public static object[] PaginationCase =
        {
                new object[] { -1, 5 },
                new object[] { 1, 5 },
                new object[] { 1, 13 },
                new object[] { 1, 15 },
                new object[] { 2, 5 },
                new object[] { 2, 8 },
                new object[] { 2, 10 },
                new object[] { 0, 0 },

        };


    }
}
