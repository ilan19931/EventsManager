using EventsManagerLogic.Classes;
using EventsManagerLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.WindowsHelpers.AdminPanel
{
    public class AddCategoriesWindowHelper
    {
        AddNewEventWindowHelper helper = new AddNewEventWindowHelper();

        public void AddNewCategory(string i_Title, int i_ParentId = -1)
        {
            string query = null;

            if (!string.IsNullOrEmpty(i_Title)) {
                if (i_ParentId == -1)
                {
                    query = $"INSERT INTO categories (title) VALUES ('{i_Title}')";
                }
                else
                {
                    query = $"INSERT INTO categories (title, parentId) VALUES ('{i_Title}', {i_ParentId})";
                }
            }

            Sql.DoQuery(query);
        }

        public ObservableCollection<Category> GetCategories(int i_Val = -1)
        {
            return helper.GetCategories(i_Val);
        }

        public void EditCategory(int i_CategoryId, string i_NewTitle)
        {
            string safeCategoryTitle = i_NewTitle;
            string query = $"UPDATE categories SET title = '{safeCategoryTitle}' WHERE id = {i_CategoryId}";

            Sql.DoQuery(query);
        }

        public void DeleteCategory(int categoryId)
        {
            if(categoryId > 0)
            {
                string query = $"DELETE FROM categories WHERE id = {categoryId} OR parentId = {categoryId}";
                Sql.DoQuery(query);
            }
        }
    }
}
