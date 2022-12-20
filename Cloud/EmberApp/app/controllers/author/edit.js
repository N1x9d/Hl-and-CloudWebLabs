import Controller from '@ember/controller';
import { inject as service } from '@ember/service';

export default Controller.extend({
  dataService: service('data'),
  actions: {
    async saveAuthor(author) {
      await this.get("dataService").updateAuthor(author);

      this.transitionToRoute('author.index');
    }
  }
});
