#Events
class Vent extends Backbone.Events

window.Vent = Vent


#Controllers
class HomeController extends Backbone.Router
	
	routes: 
		'':'getControls'
		'home':'getControls'
		'tweets/:names':'getTweets'
	
	#Route based actions
	getControls: ->
		cv = new ControlsView()
		cv.render()

	getTweets:(names) ->
		
		coll = new TwitterPostListCollection()
		
		td = new TweetDataListView( collection : coll)

		coll.fetch 
					data : {screenNames: names}
					error: (collection, response)=> alert('error')
		
		


window.SpinOpts = 
	lines: 13 #, // The number of lines to draw
	length: 20 #, // The length of each line
	width: 10 #, // The line thickness
	radius: 30 #, // The radius of the inner circle
	corners: 1 #, // Corner roundness (0..1)
	rotate: 0 #, // The rotation offset
	direction: 1 #, // 1: clockwise, -1: counterclockwise
	color: '#000' #, // #rgb or #rrggbb
	speed: 1 #, // Rounds per second
	trail: 60 #, // Afterglow percentage
	shadow: false #, // Whether to render a shadow
	hwaccel: false #, // Whether to use hardware acceleration
	className: 'LoadingSpinner' #, // The CSS class to assign to the spinner
	zIndex: 2e9 #, // The z-index (defaults to 2000000000)
	top: 'auto' #, // Top position relative to parent in px
	left: 'auto' #// Left position relative to parent in px


$(document).ready ->
	
	divLoading = $('#divLoadingSpinner')
	spinner = new Spinner(SpinOpts).spin(divLoading)
	
	#divLoading.hide()
	
	divLoading.ajaxStart( -> divLoading.fadeIn() )
	divLoading.ajaxComplete(-> divLoading.fadeOut())
	
	#app.views.messages = new MessageManager()
	#app.views.debug = new DebugMessageManager()
	
	app.controllers.main = new HomeController()
	
	Backbone.history.start()
