using Document.App.Models;

namespace Document.App.Notifiers
{
    public class DocumentNavigationComponentNotifier
    {
        public event Action<BookEntity?> UpdateDocumentNavigationWithDocumentChangedNotification = null!;
        public event Action<BookEntity?> UpdateDocumentNavigationWithChapterChangedNotification = null!;
        public event Action<BookEntity?> UpdateDocumentNavigationWithSectionChangedNotification = null!;
        public event Action<BookEntity?> UpdateDocumentNavigationWithSubSectionChangedNotification = null!;
        public event Action<BookEntity?> UpdateDocumentNavigationWithSubSubSectionChangedNotification = null!;

        #region Subscription
        public event Action<BookEntity?> UpdateDocumentNavigationWithDocumentChanged
        {
            add { UpdateDocumentNavigationWithDocumentChangedNotification += value; }
            remove { UpdateDocumentNavigationWithDocumentChangedNotification -= value; }
        }
        public event Action<BookEntity?> UpdateDocumentNavigationWithChapterChanged
        {
            add { UpdateDocumentNavigationWithChapterChangedNotification += value; }
            remove { UpdateDocumentNavigationWithChapterChangedNotification -= value; }
        }
        public event Action<BookEntity?> UpdateDocumentNavigationWithSectionChanged
        {
            add { UpdateDocumentNavigationWithSectionChangedNotification += value; }
            remove { UpdateDocumentNavigationWithSectionChangedNotification -= value; }
        }
        public event Action<BookEntity?> UpdateDocumentNavigationWithSubSectionChanged
        {
            add { UpdateDocumentNavigationWithSubSectionChangedNotification += value; }
            remove { UpdateDocumentNavigationWithSubSectionChangedNotification -= value; }
        }
        #endregion

        public void CallUpdateDocumentNavigationWithDocumentChanged(BookEntity? model)
        {
            UpdateDocumentNavigationWithDocumentChangedNotification?.Invoke(model);
        }
        public void CallUpdateDocumentNavigationWithChapterChanged(BookEntity? model)
        {
            UpdateDocumentNavigationWithChapterChangedNotification?.Invoke(model);
        }
        public void CallUpdateDocumentNavigationWithSectionChanged(BookEntity? model)
        {
            UpdateDocumentNavigationWithSectionChangedNotification?.Invoke(model);
        }
        public void CallUpdateDocumentNavigationWithSubSectionChanged(BookEntity? model)
        {
            UpdateDocumentNavigationWithSubSectionChangedNotification?.Invoke(model);
        }
        public event Action<BookEntity?> UpdateDocumentNavigationWithSubSubSectionChanged
        {
            add { UpdateDocumentNavigationWithSubSubSectionChangedNotification += value; }
            remove { UpdateDocumentNavigationWithSubSubSectionChangedNotification -= value; }
        }
        public void CallUpdateDocumentNavigationWithSubSubSectionChanged(BookEntity? model)
        {
            UpdateDocumentNavigationWithSubSubSectionChangedNotification?.Invoke(model);
        }
    }
}
