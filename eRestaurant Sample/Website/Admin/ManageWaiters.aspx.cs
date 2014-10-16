using eRestaurant.BLL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_ManageWaiters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                AddWaiter();

            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }

        }


    }

    private void AddWaiter()
    {

        Waiter person = new Waiter()
        {
            FirstName = FirstName.Text,
            LastName = LastName.Text,
            Address = Address.Text,
            Phone = Phone.Text,
            HireDate = DateTime.Parse(HireDate.Text)

        };
        DateTime temp;
        if (DateTime.TryParse(ReleaseDate.Text, out temp))
            person.ReleaseDate = temp;
        var controller = new RestaurantAdminController();
        person.WaiterID = controller.AddWaiter(person);
        WaiterID.Text = person.WaiterID.ToString();


    }



    protected void WaitersDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageLabel.Text = "";

        if (WaitersDropDown.SelectedIndex == 0)
        {
            MessageLabel.Text = "Select a Waiter before clicking 'Show Waiter Details'";

        }
        else
        {
            try
            {
                //int showWaiterID = int.Parse(WaitersDropDown.SelectedValue);
                //RestaurantAdminController controller = new RestaurantAdminController();
                //Waiter info = controller.GetWaiter(waiterId);

                //WaiterID.Text = info.WaiterID.ToString();
                //FirstName.Text = info.FirstName.ToString();
                //LastName.Text = info.LastName.ToString();
                
                //Phone.Text = info.Phone.ToString();
                //Address.Text = info.Address.ToString();
                //HireDate.Text = info.HireDate.ToShortDateString();
                //ReleaseDate.Text = info.ReleaseDate.ToString();
               
                MessageLabel.Text = "Waiter Details found";
            }
            catch (Exception ex)
            {

                MessageLabel.Text = ex.Message;
            }

        }
    }
    protected void ShowWaiter_Click(object sender, EventArgs e)
    {

    }
    protected void Add_Click(object sender, EventArgs e)
    {

        MessageUserControl.TryRun(AddWaiter, "Waiter added", " The new waiter was successfully added");


    }
    protected void Update_Click(object sender, EventArgs e)
    {

    }
    protected void Delete_Click(object sender, EventArgs e)
    {

    }
    protected void Clear_Click(object sender, EventArgs e)
    {

    }
}