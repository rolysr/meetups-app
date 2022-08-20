import NewMeetupForm from "../components/meetups/NewMeetupForm";
import { useNavigate }  from "react-router-dom";

function NewMeetupPage() {
  const navigate = useNavigate();

  function addMeetupHandler(meetupData) {
    fetch("http://localhost:5078/meetups",
    {
      method: "POST",
      body: JSON.stringify(meetupData),
      headers: {
        "Content-Type": "application/json"
      }
    }
    ).then(() => {
      navigate("/");
    });
  }

  return (
    <section>
      <h1>Add New Meetup</h1>
      <NewMeetupForm onAddMeetup={addMeetupHandler} />
    </section>
  );
}

export default NewMeetupPage;
