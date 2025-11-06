import React from 'react';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { CssBaseline, Container, AppBar, Toolbar, Typography } from '@mui/material';
import ListaComprasList from './components/ListasCompras/ListaComprasList';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

const App: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Comparador de Preços
          </Typography>
        </Toolbar>
      </AppBar>
      <Container maxWidth="lg">
        <ListaComprasList />
      </Container>
    </ThemeProvider>
  );
};

export default App;