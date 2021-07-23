describe('HomePageComponent', () => {
  beforeEach(() => {
    cy.visitApp('/');
  });

  it('Should render page with table', () => {
    cy.contains('My blog');
  });
});
