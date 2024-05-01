import { CreateBorders } from '../helpers';
import styles from './Button.module.css';

const Button = (props) => {

    return (

        // calling create borders
        // will need preset size (maybe or just send the main element to judge the size and css voodoo)
        // will need a prop string saying either button/menu/other elements that may need varying sizes
        <CreateBorders />

    )

}

export default Button;