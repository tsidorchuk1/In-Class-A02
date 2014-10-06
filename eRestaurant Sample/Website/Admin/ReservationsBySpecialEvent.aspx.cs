using eRestaurant.BLL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReservationsBySpecialEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                PopulateSpecialEvent();

            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
            }

        }
    }

    private void PopulateSpecialEvent()
    {

        // populate special events list
        RestaurantAdminController controller = new RestaurantAdminController();
        List<SpecialEvent> SpecialEvents = controller.ListAllSpecialEvents();
        SpecialEventsDropDown.DataSource = SpecialEvents;
        //SpecialEventsDropDown.DataTextField = "EventCode";
        SpecialEventsDropDown.DataValueField = "Description";
       
        SpecialEventsDropDown.DataBind();
        SpecialEventsDropDown.Items.Insert(0, "[Select an Event]");
        SpecialEventsDropDown.Items.Insert(1, "[No Event]");
    }
    protected void SpecialEventsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        MessageLabel.Text = "";

        if (SpecialEventsDropDown.SelectedIndex == 0)
        {
            MessageLabel.Text = "Select a Special before clicking 'Show Special Event Details'";

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

                MessageLabel.Text = "Special Event Details found";
            }
            catch (Exception ex)
            {

                MessageLabel.Text = ex.Message;
            }

        }
    }

}