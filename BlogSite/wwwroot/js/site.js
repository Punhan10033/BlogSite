var friend_search = document.getElementById('search_friends');
if (!friend_search.value) {
	function refreshFriendBar() {
		setInterval(function () {
			$.ajax({
				url: "/UserDefault/RefreshChatComponent",
				type: "GET",
				data: { filter: null },
				success: function (result) {
					$("#fill_data").html("");
					$("#fill_data").html(result);
					console.log("Friend bar updated!");
				},
				error: function (xhr, status, error) {
					console.error("Error occurred while refreshing friend bar: " + error);
				}
			});
		}, 2000);
	}
	refreshFriendBar();
}
else {
	
		friend_search.addEventListener('keyup', function () {
			$.ajax({
				url: "/UserDefault/RefreshChatComponent",
				type: "GET",
				data: { filter: friend_search.value },
				success: function (result) {
					$("#fill_data").html("");
					$("#fill_data").html(result);
					console.log("Friend bar updated!");
				},
				error: function (xhr, status, error) {
					console.error("Error occurred while refreshing friend bar: " + error);
				}
			});
		});
}


