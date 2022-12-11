import Controller from '@ember/controller';
import { inject as service } from '@ember/service';

export default Controller.extend({
  dataService: service('data'),
  actions: {
    async deleteAuthor(author) {
      try {
        await this.get('dataService').deleteAuthor(author);
        this.transitionToRoute('author.index');
      }
      catch (e) {
        this.send('error', new Error('Connection failed'));
      }
    }
  }
});
