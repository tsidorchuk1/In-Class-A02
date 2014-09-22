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
                PopulateCurrentWaiter();

            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }

        }


    }

    private void PopulateCurrentWaiter()
    {
       
        // populate waiter list
        RestaurantAdminController controller = new RestaurantAdminController();
        List<Waiter> waiters = controller.ListAllWaiters();
        WaitersDropDown.DataSource = waiters;
        WaitersDropDown.DataTextField = "FullName";
        WaitersDropDown.DataValueField = "WaiterID";
        WaitersDropDown.DataBind();
        WaitersDropDown.Items.Insert(0, "[Select a Waiter]");

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
                int showWaiterID = int.Parse(WaitersDropDown.SelectedValue);
                RestaurantAdminController controller = new RestaurantAdminController();
                Waiter info = controller.GetWaiter(waiterId);

                WaiterID.Text = info.WaiterID.ToString();
                FirstName.Text = info.FirstName.ToString();
                LastName.Text = info.LastName.ToString();
                
                Phone.Text = info.Phone.ToString();
                Address.Text = info.Address.ToString();
                HireDate.Text = info.HireDate.ToShortDateString();
                ReleaseDate.Text = info.ReleaseDate.ToString();
               
                MessageLabel.Text = "Waiter Details found";
            }
            catch (Exception ex)
            {

                MessageLabel.Text = ex.Message;
            }

        }
    }
}