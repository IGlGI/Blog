// tslint:disable-next-line:no-namespace
declare namespace Cypress {
  interface Chainable {
    visitApp(route: string): Cypress.Chainable;
    visitStory(story: string): Cypress.Chainable;
    getStorySharedData(cb: (shared: any) => void): Cypress.Chainable;
    getTextEditorEditable(): Cypress.Chainable;
  }
}

Cypress.Commands.add('visitApp', (route = '') => {
  cy.visit(`http://localhost:4200${route}`);
});

Cypress.Commands.add('visitStory', (story) => {
  cy.visit(`http://localhost:6006/iframe.html?id=${story}`);
});

Cypress.Commands.add('getStorySharedData', (cb: (shared: any) => void) => {
  cy.window()
    .should('have.property', 'STORY_SHARED')
    .then((shared: any) => {
      cb(shared);
    });
});

Cypress.Commands.add('getTextEditorEditable', () => cy.get('.ql-editor'));
