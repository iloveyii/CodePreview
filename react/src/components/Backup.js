import React from 'react';
import {connect} from "react-redux";
import {withRouter} from "react-router";

import Dashboard from "../../containers/Dashboard";
import models from "../../store/models";
import Alert from "../Alert";
import Toast from 'react-bootstrap/Toast';
import {settings} from "../../common/constants";
import {apiServer} from "../../common/constants";

const ERROR_TIME = 5000;

class Backup extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            model: {},
            backup: {
                message: '',
                dated: '',
                success: false,
                download_url: '',
                start: false
            },
            restore: {
                fileName: 1,
                p_id: 1,
                message: " ",
                dated: new Date().toUTCString(),
                success: false,
                start: false,
            }
        }
    }

    componentDidMount() {
        this.getBackup();
    }

    makeBackup = (e) => {
        e.preventDefault();
        const {backup} = this.state;
        backup.start = true;
        backup.success = false;
        this.setState({backup});
        setTimeout(() => {
            fetch(apiServer + '/api/v1/backups', {
                method: 'POST'
            })
                .then(response => {
                    if (response && response.ok) {
                        return response.json();
                    }
                })
                .then(data => this.setState({backup: data}))
                .catch(error => console.log(error))
        }, 1500);
    };

    getBackup = () => {
        fetch(apiServer + '/api/v1/backups/1')
            .then(response => {
                if (response && response.ok) {
                    return response.json();
                }
            })
            .then(data => {
                if (data.success) {
                    this.setState({backup: data.data[0]})
                }
            })
            .catch(error => console.log(error))
    };

    doRestore = () => {
        const {restore} = this.state;

        if(restore.fileName === 1) {
            restore.message = "Please choose zip file";
            restore.success = true;
            this.setState({restore});
            return false;
        }
        restore.start = true;
        restore.success = false;
        this.setState({restore});

        const data = new FormData();
        data.append('zip_file', this.state.restore.fileName);
        data.append('message', 'upload a zip file');
        console.log('DATA', this.state.restore, data.get('zip_file'));
        setTimeout(() => {
            fetch(apiServer + '/api/v1/backups/1', {
                method: 'PUT',
                body: data
            })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    if (data.success) {
                        this.setState({restore: data})
                    }
                })
                .catch(error => console.log(error));
        }, 4000);
    };

    setFileName = (e) => {
        const fileNameArray = this.refBackupFile.value.split('\\');
        const fileName = fileNameArray.pop();
        this.refBackupFileSpan.innerHTML = fileName;
        const {restore} = this.state;
        restore.fileName = e.target.files[0];
        this.setState({restore});
        console.log('Called setFileName', restore);
    };

    render() {
        return (
            <Dashboard>
                <div className="container mt-5">
                    <div className="row">
                        <div className="col">
                            <h3>Backup / Restore</h3>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-md-12">
                            <Alert errors={this.state.model.errors}/>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-md-12">
                            <div className="card text-left">
                                <div className="card-header">
                                    <span>
                                        <svg width="1.5em" height="1.5em" viewBox="0 0 16 16"
                                             className="bi bi-cloud-arrow-up text-success mr-2" fill="currentColor"
                                             xmlns="http://www.w3.org/2000/svg">
                                          <path fillRule="evenodd"
                                                d="M4.406 3.342A5.53 5.53 0 0 1 8 2c2.69 0 4.923 2 5.166 4.579C14.758 6.804 16 8.137 16 9.773 16 11.569 14.502 13 12.687 13H3.781C1.708 13 0 11.366 0 9.318c0-1.763 1.266-3.223 2.942-3.593.143-.863.698-1.723 1.464-2.383zm.653.757c-.757.653-1.153 1.44-1.153 2.056v.448l-.445.049C2.064 6.805 1 7.952 1 9.318 1 10.785 2.23 12 3.781 12h8.906C13.98 12 15 10.988 15 9.773c0-1.216-1.02-2.228-2.313-2.228h-.5v-.5C12.188 4.825 10.328 3 8 3a4.53 4.53 0 0 0-2.941 1.1z"/>
                                          <path fillRule="evenodd"
                                                d="M7.646 5.146a.5.5 0 0 1 .708 0l2 2a.5.5 0 0 1-.708.708L8.5 6.707V10.5a.5.5 0 0 1-1 0V6.707L6.354 7.854a.5.5 0 1 1-.708-.708l2-2z"/>
                                        </svg>
                                    </span>
                                    Backup
                                </div>
                                <div className="card-body">
                                    <h5 className="card-title">How to backup all your data ?</h5>
                                    <p className="card-text">Click the button below to backup all your data, then
                                        download it by clicking the download link, and save it to your computer.</p>
                                    <button onClick={this.makeBackup} className="btn btn-success">Backup</button>
                                    {
                                        this.state.backup.start === false && this.state.backup.download_url
                                            ? <a href={this.state.backup.download_url}
                                                 className="btn btn-outline-dark float-right">Download</a>
                                            : null
                                    }
                                </div>
                                <div className="card-footer text-muted" style={{minHeight: '40px'}}>
                                    {this.state.backup.success && this.state.backup.message + ' - ' + new Date(this.state.backup.dated).format() + ' - ' + new Date(this.state.backup.dated).timeFormat()}
                                    <span>
                                        {this.state.backup.start &&
                                        <img style={{height: '10px', width: '100%'}} src="/images/progress-bar.gif"
                                             alt="progress"/>
                                        }
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-md-12 mt-5">
                            <div className="card text-left">
                                <div className="card-header">
                                    <span>
                                        <svg width="1.5em" height="1.5em" viewBox="0 0 16 16"
                                             className="bi bi-cloud-arrow-down text-success mr-2" fill="currentColor"
                                             xmlns="http://www.w3.org/2000/svg">
                                          <path fillRule="evenodd"
                                                d="M4.406 3.342A5.53 5.53 0 0 1 8 2c2.69 0 4.923 2 5.166 4.579C14.758 6.804 16 8.137 16 9.773 16 11.569 14.502 13 12.687 13H3.781C1.708 13 0 11.366 0 9.318c0-1.763 1.266-3.223 2.942-3.593.143-.863.698-1.723 1.464-2.383zm.653.757c-.757.653-1.153 1.44-1.153 2.056v.448l-.445.049C2.064 6.805 1 7.952 1 9.318 1 10.785 2.23 12 3.781 12h8.906C13.98 12 15 10.988 15 9.773c0-1.216-1.02-2.228-2.313-2.228h-.5v-.5C12.188 4.825 10.328 3 8 3a4.53 4.53 0 0 0-2.941 1.1z"/>
                                          <path fillRule="evenodd"
                                                d="M7.646 10.854a.5.5 0 0 0 .708 0l2-2a.5.5 0 0 0-.708-.708L8.5 9.293V5.5a.5.5 0 0 0-1 0v3.793L6.354 8.146a.5.5 0 1 0-.708.708l2 2z"/>
                                        </svg>
                                    </span>
                                    Restore
                                </div>
                                <div className="card-body">
                                    <h5 className="card-title">How to restore ?</h5>
                                    <p className="card-text">Click browse and upload the zip file, then click Restore button.</p>
                                    <button onClick={this.doRestore} className="btn btn-primary">Restore</button>
                                    <span className="form-file float-right">
                                        <form action="#">
                                            <input ref={ref => this.refBackupFile = ref} onChange={this.setFileName}
                                                   accept=".zip,.json" type="file" className="form-file-input"
                                                   id="backupFile"/>
                                            <label className="form-file-label" htmlFor="backupFile">
                                                <span ref={ref => this.refBackupFileSpan = ref}
                                                      className="form-file-text">Choose backup file</span>
                                                <span className="form-file-button">Browse</span>
                                            </label>
                                        </form>
                                    </span>
                                </div>
                                <div className="card-footer text-muted" style={{minHeight: '40px'}}>
                                    {this.state.restore.success && this.state.restore.message + ' - ' + new Date(this.state.restore.dated).format() + ' - ' + new Date(this.state.restore.dated).timeFormat()}
                                    <span>
                                        {this.state.restore.start &&
                                        <img style={{height: '10px', width: '100%'}} src="/images/progress-bar.gif"
                                             alt="progress"/>
                                        }
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div
                        style={{
                            position: 'absolute',
                            top: 100,
                            right: 25,
                        }}
                    >
                        {
                            this.props.toasts.list.map(toast =>
                                <Toast
                                    key={toast.id}
                                    style={{
                                        position: 'relative',
                                        top: 100,
                                        right: 25,
                                    }}
                                    show={true} onClose={() => this.props.removeFlashMessageAction({id: toast.id})}>
                                    <Toast.Header>
                                        <img src="/images/favicon.svg" className="rounded mr-2" alt=""/>
                                        <strong className="mr-auto">{toast.type}</strong>
                                        <small className="ml-2">11 mins ago</small>
                                    </Toast.Header>
                                    <Toast.Body>{toast.message}.</Toast.Body>
                                </Toast>
                            )
                        }
                    </div>
                </div>
            </Dashboard>
        )
    }
}

const mapStateToProps = state => ({
    settings: state.settings.list[0],
    makes: state.makes,
    products: state.products,
    services: state.services,
    users: state.users,
    orders: state.orders,
    toasts: state.toasts,
});

const mapActionsToProps = {
    createAction: models.orders.actions.create,
    updateAction: models.orders.actions.update,
    readActionServices: models.services.actions.read,
    readActionOrders: models.orders.actions.read,
    addFlashMessageAction: models.toasts.actions.addList,
    removeFlashMessageAction: models.toasts.actions.deleteList,
};

Backup.defaultProps = {
    settings: settings
};

export default withRouter(connect(mapStateToProps, mapActionsToProps)(Backup));
