$(function(){
	$("#icoDown").click(
		function (){
			$("#footerMenu").slideUp("slow", function () {
				$("#icoDown").fadeOut(300, function () {
					$("#icoUp").fadeIn(300, function () { });
				});
			});
	});
	
	$("#icoUp").click(
		function (){
			$("#footerMenu").slideDown("slow", function () {
				$("#icoUp").fadeOut(300, function () {
					$("#icoDown").fadeIn(300, function () { });
				});
			});
		});
	
	
	$("#hideAllComment").toggle(
	  function () {
		 showHideBubbles('hide');
	  },
	  function () {
		  showHideBubbles('show');
	  }	);
	
	
});


function closeMessageBox(){
	$("#box1").center(); 
	$("#box1").hide();
	return false;
}

function checkCordinatest(droppable){
	var box_left   = parseInt(droppable.css("left"));
	var box_top    = parseInt(droppable.css("top"));
	alert("left: "+box_left+" top : "+box_top);
	// $("#box1").dragOff();
}

var currentEditElement;
function showPopup(obj){
// Later enhancement :  Use Jquery function offset() to get the left and top
	currentEditElement = obj.id
	var leftOff = obj.offsetLeft;
	var topOff = obj.offsetTop;

	$("#box1").show();	
	showMessageBox('sticky');
	$("#box1").css({ left:leftOff, top:topOff });
	$("#content").hide();
	$("#messageList").show();

}

function showPopupFromList(left,top,currentBubbleId){
	currentEditElement = currentBubbleId;
	var leftOff = left;
	var topOff = top;
	
	$("#box1").show();	
	showMessageBox('sticky');
	if ((leftOff==0) && (topOff==0)){
		$("#box1").center(); 
	}else{
		$("#box1").css({ left:leftOff, top:topOff });
	}
	
	$("#content").hide();
	$("#messageList").show();


return false;
}

function editMessage(){
	$("#content").show();
	$("#messageList").hide();
	$("#box1").easydrag();
	$("#box1").setHandler('handler');
	$("#box1").ondrop(function(e, element){
		editDrop($("#box1"));
	});
	return false;
}

function editDrop(drop){
//	alert(currentEditElement);
	$('#'+currentEditElement).remove();
	var newLeft   = parseInt(drop.css("left"))-10;
	var newTop    = parseInt(drop.css("top"))-10;
//	alert("Edit left: "+newLeft+" Edit top : "+newTop);
	var newDiv =  ('<div style="z-index:1000002; position:absolute; left:'+newLeft+'px; top:'+newTop+'px; cursor:pointer;"  id="'+currentEditElement+'" onclick="return showPopup(this)"; ></div>');
	$('body').append(newDiv); 
	$('#'+currentEditElement).addClass("messageBubbles");

}


jQuery.fn.center = function () { 
    this.css("position","absolute"); 
    this.css("top", ( $(window).height() - this.height() ) / 2+$(window).scrollTop() + "px"); 
    this.css("left", ( $(window).width() - this.width() ) / 2+$(window).scrollLeft() + "px"); 
    return this; 
} 





function showMessageBox(mode){
    $("#box1").show();
		if(mode=='move'){
			$("#box1").easydrag();
			$("#box1").setHandler('handler');
			$("#box1").ondrop(function(e, element){
				checkCordinatest($("#box1"));
			});
		}else{
			$("#box1").dragOff();
			$('#handler').css({ cursor:"default" });
			
		}
}


function showMessageForm(){
	 $("#box1").show();
	 	$("#commentList").removeClass("current");
		$("#addComment").addClass("current");
		$("#hideAllComment").removeClass("current");
	
	  if($('#messageList').css("display")=='block'){ 
	  	editMessage();
	  }else{
		  showMessageBox('move');
		  $("#box1").center(); 
	  }
	
	return false;
}


function closeCommentPopUp(){
	 $("#commentPopUp").slideUp();
	 return false;
}

	
	
	
	
function showHideBubbles(mode){
	if(mode=='hide'){
		$("li#hideAllComment a").html("Show All Comment");
		 $(".messageBubbles").fadeOut("slow");
	}else{
		$("li#hideAllComment a").html("Hide All Comment");
		 $(".messageBubbles").fadeIn("slow");
	}
}


/*List comments*/
function showCommentList(){
	$("#commentPopUpList").html('<p>Loading comments...</p>');
	
	$("#commentList").addClass("current");
	$("#addComment").removeClass("current");
	$("#hideAllComment").removeClass("current");
	$("#commentPopUp").slideDown();
	loadWireframeComment();
	return false;
}


/*Add comments*/
function loadWireframeComment(){
//	var subject =  $("#subject").val();
//	var message =  $("#message").val();
	$.post("../comments.ajax.php", { mode: "LoadWireframeComment" },
   	function(data){
//     alert("Data Loaded: " + data);
		if(data){
			$("#commentPopUpList").html(data);
		}else{
			$("#commentPopUpList").html('<p style="color:red;">No comments...</p>');
		}
   	});	
}
