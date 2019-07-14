const appUrl = "https://localhost:44356/api/";

let currentUserName = null;

function renderMessages(data) {
  $("#messages").empty();
  for (let messages of data) {
    $("#messages").append(
      '<div class="message d-flex justify-content-start"><strong>' +
        messages.user +
        "</strong >: " +
        messages.content +
        "</div>"
    );
  }
}

function loadMessages() {
  $.get({
    url: appUrl + "messages/all",
    success: function success(data) {
      renderMessages(data);
    },
    error: function error(error) {
      console.log(error);
    }
  });
}

function createMessages() {
  let username = currentUserName;
  let message = $("message").val();

  if (username == null) {
    alert("Choose username in order to send a message!");
    return;
  }

  if (message.lenght === 0) {
    alert("You cannot send an empty message!");
    return;
  }

  $.post({
    url: appUrl + "messages/create",
    headers: {
      "Content-Type": "application/json"
    },
    data: JSON.stringify({
      content: content,
      user: username
    }),
    success: function success(data) {
      loadMessages();
    },
    error: function error(error) {
      console.log(error);
    }
  });
}

function chooseUsername() {
  let username = $("#username").val();

  if (username.lenght === 0) {
    alert("Cannot choose an empty username!");
    return;
  }
  currentUserName = username;

  $("#username-choice").text(currentUserName);
  $("#choose-data").hide();
  $("#reset-data").show();
}

function resetUsername() {
  currentUserName = null;
  $("#choose-data").show();
  $("#reset-data").hide();
}

$("#reset-data").hide();
loadMessages();
