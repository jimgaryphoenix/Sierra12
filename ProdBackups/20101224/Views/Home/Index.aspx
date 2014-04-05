
    <h2>Here</h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>

	HERE


	<div>
		!{ using (Html.BeginForm()) { }

			!{ Html.LabelFor(m => m.FirstName) }
			!{ Html.TextBoxFor(m => m.FirstName) }
			!{ Html.ValidationMessageFor(m => m.FirstName) }
			<br /><br />

			!{ Html.LabelFor(m => m.LastName) }
			!{ Html.TextBoxFor(m => m.LastName) }
			!{ Html.ValidationMessageFor(m => m.LastName) }
			<br /><br />
			<input type="submit" value="Submit" />

		!{ } }
	</div>
