// No point for bootstrap really, menus are based on a mobile design anyway, flex-wrap should cover all aspects of this design easily with mobile screens
import '../styles/reset.css';
// Oh and fk boostrap
import '../styles/style.css';

export default function App({ Component, pageProps }) {
  return <Component {...pageProps} />;
}
