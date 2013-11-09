
class window.ControlsView extends Backbone.View
	
	el: $('#divControls')

	template: _.template $('#control-template').html()

	events:
		'click .btnGetTweets' : 'getTweets'
		

	render: =>
		@$el.html @template()
		@

	getTweets: (e) =>
		handles = @.$('.handleInput').val()
		console.log("Handles entered: " + handles)
		app.controllers.main.navigate 'tweets/' + encodeURIComponent(handles), 'trigger': true
