// Creating borders for various elements and animations
// Will likely need conditional rendering based on animation type (menu animation for hovered/selected will need a preset size on one border based on the selected link)
// May just be a switch case return based on props
// Accent for menu animation will likely need to be here
export const CreateBorders = () => {

    return (
        <></>
    )

}

// Like borders, we will need to create an accent for a hover animation for various buttons or other elements (example menu line)
export const CreateAccent = () => {

    return (
        <></>
    )

}

// Page transition animation
// GSAP - section (page), onComplete, params, redirect target
// Want it setup like so (1 = page, 0 = empty, * = Home)
// 0   1   0   1
// 1   1*  1   1
// 0   1   0   1
// Transition direction based on where you're going
export const PageTransition = () => {}