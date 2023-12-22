import {
    Button,
    FormLabel,
    FormControl,
} from 'react-bootstrap'
import {
    Form,
    Link,
    useSearchParams,
    useActionData,
    useNavigation,
} from 'react-router-dom'

export default function AuthForm() {
    const data = useActionData()
    const navigation = useNavigation()

    const [searchParams] = useSearchParams()
    const isLogin = searchParams.get('mode') === 'login'
    const isSubmitting = navigation.state === 'submitting'

    return (
        <>
            <Form method="post">
                <h1>{isLogin ? 'Log in' : 'Create a new user'}</h1>
                {data && data.errors && (
                    <ul>
                        {Object.values(data.errors).map((err) => (
                            <li key={err}>{err}</li>
                        ))}
                    </ul>
                )}
                {data && data.message && <p>{data.message}</p>}
                <div className='col-sm-4'>
                    <p>
                        <FormLabel>Email</FormLabel>
                        <FormControl type="email" name="email" required />
                    </p>
                    <p>
                        <FormLabel>Password</FormLabel>
                        <FormControl type="password" name="password" required />
                    </p>
                </div>
                <div>
                    <Link
                        style={{ marginRight: '20px' }}
                        to={`?mode=${isLogin ? 'signup' : 'login'}`}
                    >
                        {isLogin ? 'Create new user' : 'Login'}
                    </Link>
                    <Button
                        type='submit'
                        disabled={isSubmitting}
                    >
                        {isSubmitting ? 'Submitting...' : 'Save'}
                    </Button>
                </div>
            </Form >
        </>
    )
}  