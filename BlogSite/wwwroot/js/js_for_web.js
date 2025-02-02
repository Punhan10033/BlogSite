const receieverId = $("#txt_receiever").val();

function getChat(receieverId) {
	var data = { receieverId: receieverId };
	$.ajax({
		type: 'GET',
		url: '/Message/GetChat',
		data: data,
		success: function (result) {
			$("#messageContainer").html(result);
			var containerHeight = $('#messageContainer').prop('scrollHeight');
			$('#messageContainer').scrollTop(containerHeight);
		},
		error: function () {
			console.log('Error fetching messages');
		},
		complete: function () {
			setTimeout(function () { getChat(receieverId); }, 1000);
		}
	});
};
getChat(senderId);


const sender_logged = document.getElementById("txt_sender");

if (sender_logged.val !== null) {
	function refreshFriendBar() {
		$.ajax({
			url: "/Message/RefreshChatComponent",
			type: "GET",
			success: function (result) {
				$("#fill_data").html(result); 
			}
		});
	}

	setInterval(refreshFriendBar, 2000);
}




var objDiv = document.getElementById("messageContainer");
objDiv.scrollTop = objDiv.scrollHeight;


const form = document.querySelector('#messageform');
const textarea = document.querySelector('#text_area');

if (textarea.val !== null || !(/^\s*$/.test(textarea))) {
	textarea.addEventListener('keydown', function (event) {
		if (event.keyCode === 13 && !event.shiftKey) {
			event.preventDefault(); 
			var data = $("#messageform").serialize();
			$.ajax({
				type: 'POST',
				url: '/Message/SendMessage',
				contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
				data: data,
				success: function (result) {
					$("#messageContainer").html("");
					$("#messageContainer").html(result);
					$("#messageform")[0].reset(); 
				},
				error: function () {
					alert(101);
				}
			})
		}
	});
}




const receieverId = $("#txt_receiever").val();
if (receieverId !== null) {

	function getChat(receieverId) {
		var data = { receieverId: receieverId };
		$.ajax({
			type: 'GET',
			url: '/Message/GetChat',
			data: data,
			success: function (result) {
				$("#messageContainer").html(result);
				var containerHeight = $('#messageContainer').prop('scrollHeight');
				$('#messageContainer').scrollTop(containerHeight);
			},
			error: function () {
				console.log('Error fetching messages');
			},
			complete: function () {
				setTimeout(function () { getChat(receieverId); }, 1000);
			}
		});
	};
	getChat(receieverId);
}


function SubmitMessage() {
	if (textarea.val !== null || input.trim() === "") {
		var data = $("#messageform").serialize();
		$.ajax({
			type: 'POST',
			url: '/Message/SendMessage',
			contentType: 'application/x-www-form-urlencoded; charset=UTF-8', 
			data: data,
			success: function (result) {
				$("#messageContainer").html("");
				$("#messageContainer").html(result);
				$("#messageform")[0].reset(); 
			},
			error: function () {
			}
		})
	}
};

function showSubmitButton() {
	var input_submit = document.getElementById("text_area").value;
	var button_submit = document.getElementById("addBtn");
	if (input_submit !== "" || !(/^\s*$/.test(input_submit))) {
		button_submit.style.display = "block";
	} else {
		button_submit.style.display = "none";
	}
};
