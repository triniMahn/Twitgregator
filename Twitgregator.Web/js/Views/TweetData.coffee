
class window.TweetDataView extends Backbone.View
	
	tagName: 'div'
		
	template: _.template $('#tweetData-template').html()
	
	initialize: ->
		
	render: ->
		jsn = @model.toJSON()
		@$el.html @template jsn
		@


class window.TweetDataListView extends Backbone.View

	el: $('#actionWindowContainer')

	initialize: ->
		@collection.on 'all', @render, @
		

	currentAnswerView : null

	render: =>
		@collection.each @display
		@
		
	display: (data)=>
		#can we set the model and render dynamically?
		view = new TweetDataView(model : data)
		#actionWindowAns = @.$('#actionWindowAnswers')
		@$el.html( view.render().el )
		#actionWindowAns.show('slow')